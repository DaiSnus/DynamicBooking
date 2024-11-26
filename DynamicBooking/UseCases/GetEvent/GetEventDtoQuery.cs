using DynamicBooking.Doomain;
using MediatR;

namespace DynamicBooking.UseCases.GetEvent;

public record GetEventDtoQuery(Guid EventId) : IRequest<EventDto>;