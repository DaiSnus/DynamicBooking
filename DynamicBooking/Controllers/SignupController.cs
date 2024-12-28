using AutoMapper.Configuration.Annotations;
using DynamicBooking.Domain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.UseCases.Signup;
using DynamicBooking.UseCases.Signup.GetEventSignup;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

[Route("signup")]
public class SignupController : Controller
{
    private readonly IMediator mediator;

    public SignupController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("{RegistrationEventId}")]
    public async Task<IActionResult> Signup(Guid RegistrationEventId)
    {
        var e = await mediator.Send(new GetSignupEventDtoQuery(RegistrationEventId));

        var viewModel = new SignupViewModel
        {
            Event = e,
        };

        return View(viewModel);
    }

    [HttpPost("{RegistrationEventId}")]
    public async Task<Unit> Signup(SignupViewModel signupViewModel)
    {
        var result = await mediator.Send(new SignupCommand(signupViewModel));

        return Unit.Value;
    }
}
