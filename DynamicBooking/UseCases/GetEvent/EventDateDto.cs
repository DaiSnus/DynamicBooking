using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDateDto
{
    public DateTime Date { get; init; }

    public IEnumerable<TimeSlotDto> TimeSlots { get; init; }
}
