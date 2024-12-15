using DynamicBooking.Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DynamicBooking.UseCases.DeleteFile;

public class DeleteFileCommandHandler : IRequestHandler<DeleteFileCommand, Unit>
{
    private readonly IAppDbContext appDbContext;
    private readonly IWebHostEnvironment webHostEnvironment;

    public DeleteFileCommandHandler(IAppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
    {
        this.appDbContext = appDbContext;
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<Unit> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileId = request.fileId;

        var file = await appDbContext.EventsFiles.FirstAsync(eventFile => eventFile.Id == fileId);

        var path = Path.Combine(webHostEnvironment.WebRootPath, file.FilePath);

        File.Delete(path);

        appDbContext.EventsFiles.Remove(file);

        await appDbContext.SaveChangesAsync();

        return Unit.Value;
    }
}
