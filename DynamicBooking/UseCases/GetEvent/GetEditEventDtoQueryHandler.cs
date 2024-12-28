using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.GetEvent;

public class GetEditEventDtoQueryHandler : IRequestHandler<GetEditEventDtoQuery, EventDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public GetEditEventDtoQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<EventDto> Handle(GetEditEventDtoQuery request, CancellationToken cancellationToken)
    {
        var eventActionsId = request.editEventId;

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .ThenInclude(ts => ts.TimeRange)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlot)
                        .ThenInclude(ts => ts.Registrations)
                        .ThenInclude(r => r.Participant)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(e => e.EventActions.EditEventId == eventActionsId);

        var eventDto = mapper.Map<EventDto>(e);

        return eventDto;
    }
}
