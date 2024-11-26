using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.UseCases.Signup;

public class TimeSlotSignupDto
{
    public int Id { get; set; }

    public RegistrationDto Registration { get; set; }
}
