using DynamicBooking.UseCases.GetParticipants;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

[Route("registrations")]
public class RegistrationsController : Controller
{
    private readonly IMediator mediator;

    public RegistrationsController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{ResultsEventId}")]
    public async Task<IActionResult> Registrations(Guid resultsEventId)
    {
        var eventInfoWithDatesDto = await mediator.Send(new GetEventDatesQuery(resultsEventId));

        var viewModel = new RegistrationsViewModel { EventInfoAndDates = eventInfoWithDatesDto };

        return View(viewModel);
    }

    [HttpGet("{EventDateId}/participants")]
    public async Task<IActionResult> Participants(Guid eventDateId)
    {
        var participantsDto = await mediator.Send(new GetParticipantsDtoQuery(eventDateId));

        var viewModel = new ParticipantsViewModel { Participants = participantsDto };

        return View(viewModel);
    }
}
