using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;

namespace DynamicBooking.UseCases.CreateForm;

public record CreateFormCommand(FormViewModel viewModel) : IRequest<EventActionsIdDto>;