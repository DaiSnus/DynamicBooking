using MediatR;

namespace DynamicBooking.UseCases.Delete;

public record DeleteEventDateCommand(int index, Guid editEventId): IRequest<Unit>;