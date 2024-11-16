using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.UseCases.CreateForm;

public record CreateFormCommand(EventDto eventDto) : IRequest<EventUrls>;