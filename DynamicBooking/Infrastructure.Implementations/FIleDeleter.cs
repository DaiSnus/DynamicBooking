using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using MediatR;
using System.IO;

namespace DynamicBooking.Infrastructure.Implementations;

public class FIleDeleter : IFileDeleter
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public FIleDeleter(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public Unit DeleteFile(IEnumerable<EventFileDto> deletedFiles)
    {
        foreach (var deletedFile in deletedFiles)
        {
            var path = deletedFile.FilePath;

            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Delete))
            {
                File.Delete(path);
            }
        }

        return Unit.Value;
    }
}
