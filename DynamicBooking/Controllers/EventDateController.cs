using DynamicBooking.UseCases.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

[Route("eventdate")]
public class EventDateController : Controller
{
    private readonly IMediator mediator;

    public EventDateController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("delete")]
    public async Task<Unit> Delete(DeleteEventDateCommand command)
    {
        await mediator.Send(command);

        return Unit.Value;
    }
}
