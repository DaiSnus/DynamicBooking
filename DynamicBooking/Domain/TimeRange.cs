using DynamicBooking.Doomain;

namespace DynamicBooking.Domain;

public class TimeRange
{
    public int Id { get; set; }

    public int TimeSlotId { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
