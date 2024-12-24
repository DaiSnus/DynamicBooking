using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;

namespace DynamicBooking.UseCases.EditForm;

public record EditFormCommand(EditViewModel viewModel) : IRequest<EventActionsIdDto>;