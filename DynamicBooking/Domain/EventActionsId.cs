using DynamicBooking.Doomain;

namespace DynamicBooking.Domain;

public class EventActionsId
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public Guid RegistrationEventId { get; set; }

    public Guid ResultsId { get; set; }

    public Guid EditEventId { get; set; }
}
