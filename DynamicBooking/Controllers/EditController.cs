using DynamicBooking.UseCases.EditForm;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DynamicBooking.Controllers;

[Route("edit")]
public class EditController : Controller
{
    private readonly IMediator mediator;

    public EditController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{EditEventId}")]
    public async Task<IActionResult> Edit(Guid EditEventId)
    {
        var e = await mediator.Send(new GetEventDtoQuery(EditEventId));

        var viewModel = new FormViewModel
        {
            Event = e
        };

        return View(viewModel);
    }

    [HttpPost("{EditEventID}")]
    public async Task<IActionResult> Edit(FormViewModel model)
    {
        var command = new EditFormCommand(model.Event);

        var eventActions = await mediator.Send(command);

        return RedirectToAction("References", "References", eventActions);
    }
}
