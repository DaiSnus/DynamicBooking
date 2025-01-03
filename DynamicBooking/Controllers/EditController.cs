﻿using DynamicBooking.UseCases.EditForm;
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
        var e = await mediator.Send(new GetEditEventDtoQuery(EditEventId));

        var viewModel = new EditViewModel
        {
            Event = e
        };

        return View(viewModel);
    }

    [HttpPost("{EditEventID}")]
    public async Task<IActionResult> Edit(EditViewModel model)
    {
        var command = new EditFormCommand(model);

        var eventActions = await mediator.Send(command);

        return RedirectToAction("References", "References", eventActions);
    }
}
