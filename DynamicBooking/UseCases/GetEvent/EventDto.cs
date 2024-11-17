namespace DynamicBooking.UseCases.GetEvent;

public class EventDto
{
    public UserDto Owner { get; init; }

    public EventActionsIdDto EventActions { get; set; }

    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<EventFileDto>? FormFiles { get; set; }

    public IEnumerable<EventDateDto> EventDates { get; init; } = [];

    public IEnumerable<EventFieldDto>? OptionalFields { get; init; }
}
