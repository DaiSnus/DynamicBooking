using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.UseCases.EditForm;

public record EditFormCommand(EventDto eventDto) : IRequest<EventActionsIdDto>;