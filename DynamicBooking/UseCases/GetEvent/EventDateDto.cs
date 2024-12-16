using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDateDto
{
    public Guid Id { get; set; }

    public DateOnly Date { get; init; }

    public TimeSlotDto TimeSlot { get; init; }
}