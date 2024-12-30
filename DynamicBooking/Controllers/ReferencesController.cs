using DynamicBooking.Domain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

public class ReferencesController : Controller
{
    private readonly IMediator mediator;

    public ReferencesController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("references")]
    public IActionResult References(EventActionsIdDto eventActions)
    {
        var viewModel = new ReferencesViewModel
        {
            EventActions = eventActions
        };

        return View(viewModel);
    }
}