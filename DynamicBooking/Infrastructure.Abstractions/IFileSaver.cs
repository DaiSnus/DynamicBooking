using DynamicBooking.UseCases.GetEvent;

namespace DynamicBooking.Infrastructure.Abstractions;

public interface IFileSaver
{
    Task<IEnumerable<EventFileDto>> SaveFilesAndGetDoomainInstances(IFormFileCollection files);
}