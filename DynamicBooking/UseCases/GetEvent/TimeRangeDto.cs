namespace DynamicBooking.UseCases.GetEvent;

public class TimeRangeDto
{
    public TimeOnly StartTime { get; init; }

    public TimeOnly EndTime { get; init; }
}
