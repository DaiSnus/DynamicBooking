using DynamicBooking.UseCases.DeleteFile;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

[Route("file")]
public class FileController : Controller
{
    private readonly IMediator mediator;

    public FileController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("delete")]
    public async Task<Unit> Delete(DeleteFileCommand command)
    {
        await mediator.Send(command);

        return Unit.Value;
    }
}
