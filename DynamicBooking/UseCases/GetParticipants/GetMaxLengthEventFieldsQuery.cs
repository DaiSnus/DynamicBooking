using MediatR;

namespace DynamicBooking.UseCases.GetParticipants;

public record GetMaxLengthEventFieldsQuery(Guid eventDateId) : IRequest<int>;