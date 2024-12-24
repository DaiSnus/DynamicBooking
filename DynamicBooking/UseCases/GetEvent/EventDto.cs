using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.GetEvent;

public class EventDto
{
    public UserDto Owner { get; init; }

    public EventActionsIdDto EventActions { get; set; }

    public string Title { get; init; }

    public string Description { get; init; }

    public IEnumerable<EventFileDto>? FormFiles { get; set; }

    public IEnumerable<EventDateDto> EventDates { get; set; }

    public IEnumerable<EventFieldDto> OptionalFields { get; set; }
}