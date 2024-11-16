using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDateDto
{
    public DateTime Date { get; init; }

    public ICollection<TimeSlotDto> TimeSlots { get; init; } = [];
}
