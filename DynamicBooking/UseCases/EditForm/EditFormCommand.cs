using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.UseCases.EditForm;

public record EditFormCommand(Guid eventId) : IRequest<Unit>;