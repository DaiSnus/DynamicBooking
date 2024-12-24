using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventFieldDto
{
    public Guid Id { get; set; }

    public string Title { get; init; }

    public string Type { get; init; }
}
