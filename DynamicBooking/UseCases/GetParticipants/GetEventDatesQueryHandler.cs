using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.GetParticipants;

public class GetEventDatesQueryHandler : IRequestHandler<GetEventDatesQuery, EventInfoAndDatesDto>
{
    private readonly IAppDbContext appDbContext;

    public GetEventDatesQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<EventInfoAndDatesDto> Handle(GetEventDatesQuery request, CancellationToken cancellationToken)
    {
        var resultsId = request.resultsId;

        var e = await appDbContext.Events.Include(e => e.EventDates)
                                         .ThenInclude(ed => ed.TimeSlot)
                                         .ThenInclude(ts => ts.TimeRange)
                                         .FirstAsync(e => e.EventActions.ResultsEventId == resultsId);

        return new EventInfoAndDatesDto { EventDates = e.EventDates, Title = e.Title };
    }
}