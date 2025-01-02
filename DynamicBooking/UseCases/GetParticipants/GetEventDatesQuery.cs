using DynamicBooking.Doomain;
using MediatR;

namespace DynamicBooking.UseCases.GetParticipants;

public record GetEventDatesQuery(Guid resultsId) : IRequest<EventInfoAndDatesDto>;