using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.UseCases.Signup;

public class EventSignupDto
{
    public EventActionsIdDto EventActions { get; set; }

    public EventDateSignupDto EventDates { get; set; }

    public EventFieldSignupDto? OptionalFields { get; set; }
}