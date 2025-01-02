using MediatR;

namespace DynamicBooking.UseCases.GetParticipants;

public record GetParticipantsDtoQuery(Guid eventDateId) : IRequest<ParticipantsDto>;