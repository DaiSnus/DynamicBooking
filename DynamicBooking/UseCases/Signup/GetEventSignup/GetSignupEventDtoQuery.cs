using DynamicBooking.Doomain;
using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.UseCases.Signup.GetEventSignup;

public record GetSignupEventDtoQuery(Guid registrationEventId) : IRequest<EventDto>;