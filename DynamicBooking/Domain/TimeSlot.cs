using DynamicBooking.Domain;

namespace DynamicBooking.Doomain;

public class TimeSlot
{
    public Guid Id { get; set; }

    public Guid EventDateId {  get; set; }

    public EventDate EventDate { get; set; }

    public TimeRange TimeRange { get; set; }

    public IEnumerable<Registration>? Registrations { get; set; }

    public int AvailableSeats { get; set; }
}
