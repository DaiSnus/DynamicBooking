using DynamicBooking.Doomain;
using MediatR;

namespace DynamicBooking.UseCases.GetEvent;

public record GetEditEventDtoQuery(Guid editEventId) : IRequest<EventDto>;