using DynamicBooking.Doomain;

namespace DynamicBooking.UseCases.Signup;

public class RegistrationSuccessDto
{
    public bool IsSuccess { get; set; }

    public EventDate EventDate { get; set; }
}