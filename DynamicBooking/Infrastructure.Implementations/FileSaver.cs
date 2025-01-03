﻿using DynamicBooking.Infrastructure.Abstractions;
using DynamicBooking.UseCases.GetEvent;
using Microsoft.AspNetCore.Hosting;

namespace DynamicBooking.Infrastructure.Implementations;

public class FileSaver : IFileSaver
{
    private readonly IWebHostEnvironment webHostEnvironment;

    public FileSaver(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        this.webHostEnvironment = webHostEnvironment;
    }

    public async Task<IEnumerable<EventFileDto>> SaveFilesAndGetDoomainInstancesAsync(IFormFileCollection files, string directory)
    {
        var eventFiles = new List<EventFileDto>();

        foreach (var uploadedFile in files)
        {
            if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, directory)))
            {
                Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, directory));
            }

            var path = Path.Combine(webHostEnvironment.WebRootPath, directory, uploadedFile.FileName + Guid.NewGuid().ToString());

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await uploadedFile.CopyToAsync(fileStream);
            }
            var file = new EventFileDto { FileName = uploadedFile.FileName, FilePath = path };

            eventFiles.Add(file);
        }

        return eventFiles;
    }
}
