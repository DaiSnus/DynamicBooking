namespace DynamicBooking.UseCases.GetEvent;

public class TimeSlotDto
{
    public TimeOnly Time { get; init; }

    public int AvailableSeats { get; init; }
}
