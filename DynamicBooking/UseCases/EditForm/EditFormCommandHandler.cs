using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.EditForm;

public class EditFormCommandHandler : IRequestHandler<EditFormCommand, EventActionsIdDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public EditFormCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }


    public async Task<EventActionsIdDto> Handle(EditFormCommand request, CancellationToken cancellationToken)
    {
        var editDto = request.eventDto;

        var editEvent = mapper.Map<Event>(editDto);

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(ea => ea.EventActions.EditEventId == editDto.EventActions.EditEventId);

        e.Owner.Name = editEvent.Owner.Name;
        e.Owner.Surname = editEvent.Owner.Surname;
        e.Owner.Patronymic = editEvent.Owner.Patronymic;
        e.Owner.PhoneNumber = editEvent.Owner.PhoneNumber;
        e.Owner.Email = editEvent.Owner.Email;
        e.Title = editEvent.Title;
        e.Description = editEvent.Description;
        e.FormFiles = editEvent.FormFiles;
        e.EventDates = editEvent.EventDates;
        e.OptionalFields = editEvent.OptionalFields;

        await appDbContext.SaveChangesAsync();

        return editDto.EventActions;
    }
}
