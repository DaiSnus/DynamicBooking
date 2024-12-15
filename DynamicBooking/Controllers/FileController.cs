using DynamicBooking.UseCases.DeleteFile;
using DynamicBooking.UseCases.EditForm;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DynamicBooking.Controllers;

public class FileController : Controller
{
    private readonly IMediator mediator;
    
    public FileController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("file/delete")]
    public async Task<Unit> DeleteFile(DeleteFileCommand command)
    {
        await mediator.Send(command);

        return Unit.Value;
    }
}
