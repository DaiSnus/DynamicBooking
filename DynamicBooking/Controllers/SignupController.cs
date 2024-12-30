using AutoMapper.Configuration.Annotations;
using DynamicBooking.Domain;
using DynamicBooking.HttpContextServices;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.UseCases.Signup;
using DynamicBooking.UseCases.Signup.GetEventSignup;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

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
    public async Task<IActionResult> Signup(SignupViewModel signupViewModel)
    {
        var registrationResultDtos = await mediator.Send(new SignupCommand(signupViewModel));

        TempData.AddRegistrationSeccess(registrationResultDtos);

        return RedirectToAction("results");
    }

    [HttpGet("results")]
    public IActionResult ResultRegistration()
    {
        var registrationResultDtos = TempData.GetRegistrationSuccessDtos();

        return View(new ResultRegistrationViewModel { RegistrationSucesses = registrationResultDtos });
    }
}
