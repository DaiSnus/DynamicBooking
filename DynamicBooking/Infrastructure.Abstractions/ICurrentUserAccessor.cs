namespace DynamicBooking.Infrastructure.Abstractions;

public interface ICurrentUserAccessor
{
    Guid GetCurrentUserId();
}
