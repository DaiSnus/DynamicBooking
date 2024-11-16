using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDto
{
    public UserDto Owner { get; init; }

    public EventActionsDto EventActions { get; set; }

    public string Title { get; init; }

    public string Description { get; init; }

    public ICollection<EventFileDto>? FormFiles { get; set; }

    public ICollection<EventDateDto> EventDates { get; init; } = [];

    public ICollection<EventFieldDto>? OptionalFields { get; init; }
}
