using MediatR;

namespace DynamicBooking.UseCases.Delete;

public record DeleteEventDateCommand(Guid editEventId, Guid eventDateId): IRequest<Unit>;