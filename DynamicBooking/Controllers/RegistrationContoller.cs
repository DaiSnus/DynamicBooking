using MediatR;

namespace DynamicBooking.Controllers;

public class RegistrationContoller
{
    private readonly IMediator mediator;

    public RegistrationContoller(IMediator mediator)
    {
        this.mediator = mediator;
    }


}
