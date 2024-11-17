using AutoMapper;
using DynamicBooking.Domain;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.EditForm;

public class EditFormCommandHandler : IRequestHandler<EditFormCommand, EventActionsDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public EditFormCommandHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }


    public async Task<EventActionsDto> Handle(EditFormCommand request, CancellationToken cancellationToken)
    {
        var editDto = request.eventDto;

        var editEvent = mapper.Map<Event>(request.eventDto);

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlots)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(ea => ea.EventActions.EditEventId == editDto.EventActions.EditEventId);

        e.EventActions = editEvent.EventActions;
        e.Owner = editEvent.Owner;
        e.Title = editEvent.Title;
        e.Description = editEvent.Description;
        e.FormFiles = editEvent.FormFiles;
        e.EventDates = editEvent.EventDates;
        e.OptionalFields = editEvent.OptionalFields;

        return editDto.EventActions;
    }
}
