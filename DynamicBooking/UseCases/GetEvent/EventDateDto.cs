using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDateDto
{
    public int Id { get; init; }

    public DateOnly Date { get; init; }

    public TimeSlotDto TimeSlot { get; init; }
}