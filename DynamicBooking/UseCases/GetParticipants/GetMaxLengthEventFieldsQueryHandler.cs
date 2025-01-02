using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.GetParticipants;

public class GetMaxLengthEventFieldsqueryHandler : IRequestHandler<GetMaxLengthEventFieldsQuery, int>
{
    private readonly IAppDbContext appDbContext;

    public GetMaxLengthEventFieldsqueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }
    public async Task<int> Handle(GetMaxLengthEventFieldsQuery request, CancellationToken cancellationToken)
    {
        var eventDateId = request.eventDateId;

        var eventDates = await appDbContext.EventsDate.Include(eventDate => eventDate.Event)
                                                      .ThenInclude(e => e.OptionalFields)
                                                      .FirstAsync(eventDate => eventDate.Id == eventDateId);

        return eventDates.Event.OptionalFields.Count();
    }
}