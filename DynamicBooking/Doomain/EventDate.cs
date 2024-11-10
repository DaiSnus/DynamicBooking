namespace DynamicBooking.Doomain;

public class EventDate
{
    public int Id { get; set; }

    public Guid EventId { get; set; }

    public Event Event { get; set; }

    public DateTime Date { get; set; }

    public IEnumerable<TimeSlot> TimeSlots { get; set; }
}
