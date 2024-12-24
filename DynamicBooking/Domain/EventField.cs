namespace DynamicBooking.Doomain;

public class EventField
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public string Title { get; set; }

    public string Type { get; set; }

    public IEnumerable<EventFieldValue> EventFieldValues { get; set; }
}
