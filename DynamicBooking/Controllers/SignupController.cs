using AutoMapper.Configuration.Annotations;
using DynamicBooking.Domain;
using DynamicBooking.UseCases.GetEvent;
using DynamicBooking.UseCases.Signup;
using DynamicBooking.UseCases.Signup.GetEventSignup;
using DynamicBooking.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

public class SignupController : Controller
{
    private readonly IMediator mediator;

    public SignupController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("signup/{RegistrationEventId}")]
    public async Task<IActionResult> Signup(Guid RegistrationEventId)
    {
        var e = await mediator.Send(new GetEventQuery(RegistrationEventId));

        var viewModel = new SignupViewModel
        {
            Event = e,
            EventSignup = new EventSignupDto
            {
                EventActions = new EventActionsIdDto
                {
                    EditEventId = e.EventActions.EditEventId,
                    RegistrationEventId = RegistrationEventId,
                    ResultsId = e.EventActions.ResultsId,
                }
            }
        };

        return View(viewModel);
    }

    [HttpPost("signup/{RegistrationEventId}")]
    public async Task<IActionResult> Signup(SignupViewModel signupViewModel)
    {
        var eventSignUpDto = signupViewModel.EventSignup;

        var result = mediator.Send(eventSignUpDto);

        return RedirectToAction("references", "references", eventSignUpDto.EventActions);
    }
}
