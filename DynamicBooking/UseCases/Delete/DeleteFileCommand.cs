using MediatR;

namespace DynamicBooking.UseCases.DeleteFile;

public record DeleteFileCommand(Guid fileId) : IRequest<Unit>;