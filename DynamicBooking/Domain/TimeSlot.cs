using DynamicBooking.Domain;

namespace DynamicBooking.Doomain;

public class TimeSlot
{
    public int Id { get; set; }

    public int EventDateId {  get; set; }

    public EventDate EventDate { get; set; }

    public TimeRange TimeRange { get; set; }

    public IEnumerable<Registration>? Registrations { get; set; }

    public int AvailableSeats { get; set; }
}
