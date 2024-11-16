namespace DynamicBooking.Doomain;

public class EventFieldValue
{
    public int Id { get; set; }

    public int EventFieldId { get; set; }

    public EventField EventField { get; set; }

    public string Value { get; set; }
}
