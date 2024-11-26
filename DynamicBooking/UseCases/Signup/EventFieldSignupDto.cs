using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.UseCases.Signup;

public class EventFieldSignupDto
{
    public int Id { get; set; }

    public EventFieldValueDto EventFieldValues { get; set; }
}