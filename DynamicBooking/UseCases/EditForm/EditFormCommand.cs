using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;

namespace DynamicBooking.UseCases.EditForm;

public record EditFormCommand(FormViewModel viewModel) : IRequest<EventActionsIdDto>;