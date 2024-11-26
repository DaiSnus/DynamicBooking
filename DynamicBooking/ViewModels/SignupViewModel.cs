using DynamicBooking.Doomain;
using DynamicBooking.UseCases.Signup;

namespace DynamicBooking.ViewModels;

public class SignupViewModel
{
    public Event Event { get; set; }

    public EventSignupDto EventSignup { get; set; }
}
