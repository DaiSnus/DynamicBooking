using DynamicBooking.Doomain;
using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.Signup.GetEventSignup;

public class GetEventQueryHandler : IRequestHandler<GetEventQuery, Event>
{
    private readonly IAppDbContext appDbContext;
    
    public GetEventQueryHandler(IAppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<Event> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        var registrationEventId = request.registrationEventId;

        var e = await appDbContext.Events
                       .Include(e => e.EventActions)
                       .Include(e => e.Owner)
                       .Include(e => e.EventDates)
                       .ThenInclude(ed => ed.TimeSlot)
                       .ThenInclude(ts => ts.Registrations)
                       .ThenInclude(r => r.Participant)
                       .Include(e => e.EventDates)
                       .ThenInclude(ed => ed.TimeSlot)
                       .ThenInclude(ts => ts.TimeRange)
                       .Include(e => e.FormFiles)
                       .Include(e => e.OptionalFields)
                       .ThenInclude(of => of.EventFieldValues)
                       .FirstAsync(e => e.EventActions.RegistrationEventId == registrationEventId);

        return e;
    }
}