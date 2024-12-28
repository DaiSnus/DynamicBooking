using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.UseCases.Signup;

public class SignupEventDto
{
    public UserDto Participant { get; init; }

    public EventActionsIdDto EventActions { get; set; }

    public IEnumerable<Guid> SelectedEventDates { get; set; }

    public IFormFileCollection? FormFiles { get; set; }

    public IEnumerable<EventFieldValueDto> EventFieldsValue { get; set; }
}
