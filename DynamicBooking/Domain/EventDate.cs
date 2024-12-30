namespace DynamicBooking.Doomain;

public class EventDate
{
    public Guid Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public DateOnly Date { get; set; }

    public TimeSlot TimeSlot { get; set; }
}