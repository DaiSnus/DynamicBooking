using DynamicBooking.UseCases.GetEvent;
using MediatR;

namespace DynamicBooking.Infrastructure.Abstractions;

public interface IFileDeleter
{
    Unit DeleteFile(IEnumerable<EventFileDto> deletedFiles);
}