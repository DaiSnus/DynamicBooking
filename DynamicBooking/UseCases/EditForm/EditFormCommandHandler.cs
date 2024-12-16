using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.Infrastructure.Implementations;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DynamicBooking.UseCases.EditForm;

public class EditFormCommandHandler : IRequestHandler<EditFormCommand, EventActionsIdDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;
    private readonly IFileSaver fileSaver;

    public EditFormCommandHandler(IAppDbContext appDbContext, IMapper mapper, IFileSaver fileSaver)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
        this.fileSaver = fileSaver;
    }


    public async Task<EventActionsIdDto> Handle(EditFormCommand request, CancellationToken cancellationToken)
    {
        var viewModel = request.viewModel;
        var eventDto = viewModel.Event;

        if (viewModel.EventFiles != null && viewModel.EventFiles.Count > 0)
        {
            eventDto.FormFiles = await fileSaver.SaveFilesAndGetDoomainInstances(viewModel.EventFiles);
        }

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(ea => ea.EventActions.EditEventId == eventDto.EventActions.EditEventId, cancellationToken);

        eventDto.EventDates = viewModel.EventDates;
        eventDto.OptionalFields = viewModel.OptionalFields;

        var eventD = mapper.Map<Event>(eventDto);

        var eventDatesDto = eventD.EventDates.Where(ed => ed.EventId == Guid.Empty);
        var eventDates = e.EventDates;
        foreach (var item in eventDatesDto)
        {
            item.Event = e;
            if (e.EventDates.Where(ed => ed.Id == item.Id).Count() == 0)
            {
                e.EventDates.Append(item);
            }
        }
        //foreach (var eventDate in eventDates)
        //{
        //    var eventDateDto = eventDatesDto.FirstOrDefault(ed => ed.Id == eventDate.Id);

        //    if (eventDateDto != null)
        //    {
        //        eventDateDto.TimeSlot.EventDateId = eventDate.TimeSlot.EventDateId;
        //        eventDateDto.EventId = eventDate.EventId;
        //        eventDateDto.Event = eventDate.Event;
        //        eventDateDto.TimeSlot.EventDate = eventDate.TimeSlot.EventDate;
        //        eventDateDto.TimeSlot.TimeRange.TimeSlot = eventDate.TimeSlot;
        //    }
        //}
        //var eventId = eventDatesDto.First().EventId;
        //var eventDateId = eventDatesDto.First().TimeSlot.EventDateId;
        //foreach (var eventDateDto in eventDatesDto.Where(ed => ed.EventId == Guid.Empty))
        //{
        //    eventDateDto.Event = eventD;
        //    eventDateDto.TimeSlot.EventDate = eventDateDto;
        //    eventDateDto.TimeSlot.TimeRange.TimeSlot = eventDateDto.TimeSlot;
        //    eventDateDto.EventId = eventId;
        //    eventDateDto.TimeSlot.EventDateId = eventDateId;
        //}

        appDbContext.EventsDate.RemoveRange(eventDates.Where(ed => ed.EventId == e.Id));

        await appDbContext.EventsDate.AddRangeAsync(eventDatesDto);

        e.Owner.Surname = eventDto.Owner.Surname;
        e.Owner.Name = eventDto.Owner.Name;
        e.Owner.Email = eventDto.Owner.Email;
        e.Owner.PhoneNumber = eventDto.Owner.PhoneNumber;

        await appDbContext.SaveChangesAsync(cancellationToken);

        return eventDto.EventActions;
    }
}
