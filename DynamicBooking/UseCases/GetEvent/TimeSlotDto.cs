using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class TimeSlotDto
{
    public TimeRangeDto TimeRange { get; init; }

    public int AvailableSeats { get; init; }

    public IEnumerable<RegistrationDto>? Registrations { get; set; }
}
