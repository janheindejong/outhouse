using OutHouse.Server.Models;

namespace OutHouse.Server.Identity
{
    internal sealed class UserContext(HttpContext httpContext)
        : IUser
    {
        public Guid Id =>
            httpContext
                .User
                .GetUserId();

        public bool IsAuthenticated =>
            httpContext
                .User
                .Identity?
                .IsAuthenticated ??
            throw new ApplicationException("User context is unavailable");
    }
}
