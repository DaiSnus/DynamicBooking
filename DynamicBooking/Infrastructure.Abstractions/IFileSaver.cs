using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.Infrastructure.Abstractions;

public interface IFileSaver
{
    Task<IEnumerable<EventFileDto>> SaveFilesAndGetDoomainInstancesAsync(IFormFileCollection files, string directory);
}