using MediatR;

namespace DynamicBooking.UseCases.GetEvent;

public record GetEventQuery() : IRequest<EventDto>;
