namespace DynamicBooking.Doomain;

public class TimeSlot
{
    public int Id { get; set; }

    public int EventDateId {  get; set; }

    public EventDate EventDate { get; set; }

    public TimeOnly Time {  get; set; }

    public int AvailableSeats {  get; set; }
}
