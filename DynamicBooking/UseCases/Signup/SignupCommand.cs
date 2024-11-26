using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.UseCases.Signup;

public record SignupCommand(EventSignupDto eventSignupDto) : IRequest<Unit>;