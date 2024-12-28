using DynamicBooking.Doomain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.UseCases.Signup;

namespace DynamicBooking.ViewModels;

public class SignupViewModel
{
    public EventDto Event { get; set; }

    public UserDto Participant { get; init; }

    public IEnumerable<Guid> SelectedEventDatesIds { get; set; }

    public IFormFileCollection ParticipantFiles { get; set; }

    public IEnumerable<EventFieldValueDto> EventFieldsValue { get; set; }
}