using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace DynamicBooking.Controllers;

public class EditController : Controller
{
    private readonly IMediator mediator;

    public EditController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("edit")]
    public IActionResult Edit(string url)
    {
        return View(new FormViewModel());
    }
}
