using DynamicBooking.Doomain;
using MediatR;

namespace DynamicBooking.UseCases.Signup.GetEventSignup;

public record GetEventQuery(Guid registrationEventId) : IRequest<Event>;