using DynamicBooking.Infrastructure.Abstractions;
using System.Security.Claims;

namespace DynamicBooking.Infrastructure.Implementations;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        if (httpContextAccessor.HttpContext == null)
        {
            throw new InvalidOperationException("Cannot get HTTP context.");
        }

        var idValue = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(idValue, out var userId))
        {
            throw new InvalidOperationException("Cannot parse user ID.");
        }

        return userId;
    }
}
