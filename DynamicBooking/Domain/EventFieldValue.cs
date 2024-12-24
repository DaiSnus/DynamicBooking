namespace DynamicBooking.Doomain;

public class EventFieldValue
{
    public Guid Id { get; set; }

    public Guid EventFieldId { get; set; }

    public EventField EventField { get; set; }

    public string Value { get; set; }
}
