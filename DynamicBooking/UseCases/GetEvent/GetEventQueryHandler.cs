using AutoMapper;
using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.GetEvent;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventDto>
{
    private readonly IAppDbContext appDbContext;
    private readonly IMapper mapper;

    public GetEventQueryHandler(IAppDbContext appDbContext, IMapper mapper)
    {
        this.appDbContext = appDbContext;
        this.mapper = mapper;
    }

    public async Task<EventDto> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var eventActionsId = request.EventId;

        var e = await appDbContext.Events
                        .Include(e => e.EventActions)
                        .Include(e => e.Owner)
                        .Include(e => e.EventDates)
                        .ThenInclude(ed => ed.TimeSlots)
                        .Include(e => e.FormFiles)
                        .Include(e => e.OptionalFields)
                        .ThenInclude(of => of.EventFieldValues)
                        .FirstAsync(ea => ea.EventActions.EditEventId == eventActionsId);

        var eventDto = mapper.Map<EventDto>(e);

        return eventDto;
    }
}
