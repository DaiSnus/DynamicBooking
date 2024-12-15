using DynamicBooking.UseCases.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

public class EventDateController : Controller
{
    private readonly IMediator mediator;

    public EventDateController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("")]
    public async Task<Unit> DeleteEventDate(DeleteEventDateCommand command)
    {
        return Unit.Value;
    }
}
