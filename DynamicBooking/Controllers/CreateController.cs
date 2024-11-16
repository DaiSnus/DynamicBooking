using DynamicBooking.UseCases.CreateForm;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DynamicBooking.Controllers;

public class CreateController : Controller
{
    private readonly IMediator mediator;

    public CreateController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FormViewModel model)
    {
        var command = new CreateFormCommand(model.Event);

        var eventUrls = await mediator.Send(command);

        var viewModel = new CreatedViewModel
        {
            EventUrls = eventUrls
        };

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View(new FormViewModel());
    }
}
