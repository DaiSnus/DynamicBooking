using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;

namespace DynamicBooking.UseCases.Signup;

public record SignupCommand(SignupViewModel signupViewModel) : IRequest<IEnumerable<RegistrationSuccessDto>>;