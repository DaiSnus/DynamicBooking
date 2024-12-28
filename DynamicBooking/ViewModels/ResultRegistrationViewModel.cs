using DynamicBooking.UseCases.Signup;

namespace DynamicBooking.ViewModels;

public class ResultRegistrationViewModel
{
    public IEnumerable<RegistrationSuccessDto> RegistrationSucesses { get; set; }
}
