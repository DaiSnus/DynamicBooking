using DynamicBooking.Doomain;

namespace DynamicBooking.Domain;

public class TimeRange
{
    public Guid Id { get; set; }

    public Guid TimeSlotId { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }
}
