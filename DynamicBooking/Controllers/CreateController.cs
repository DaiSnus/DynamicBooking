using DynamicBooking.UseCases.CreateForm;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DynamicBooking.Controllers;

[Route("create")]
public class CreateController : Controller
{
    private readonly IMediator mediator;

    public CreateController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost()]
    public async Task<IActionResult> Create(FormViewModel model)
    {
        var command = new CreateFormCommand(model);

        var eventActions = await mediator.Send(command);

        return RedirectToAction("References", "References", eventActions);
    }

    [HttpGet()]
    public IActionResult Create()
    {
        return View(new FormViewModel());
    }
}
