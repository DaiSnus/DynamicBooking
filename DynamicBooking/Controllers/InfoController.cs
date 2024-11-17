using DynamicBooking.Domain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

public class InfoController : Controller
{
    private readonly IMediator mediator;

    public InfoController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("info")]
    public IActionResult Info(EventActionsIdDto eventActions)
    {
        var viewModel = new InfoViewModel
        {
            EventActions = eventActions
        };

        return View(viewModel);
    }
}
