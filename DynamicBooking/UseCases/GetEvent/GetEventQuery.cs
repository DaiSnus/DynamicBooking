using DynamicBooking.Doomain;
using MediatR;

namespace DynamicBooking.UseCases.GetEvent;

public record GetEventQuery(Guid EventId) : IRequest<EventDto>;