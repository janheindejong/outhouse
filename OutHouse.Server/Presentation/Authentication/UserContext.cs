using OutHouse.Server.Service;
using OutHouse.Server.Models;

namespace OutHouse.Server.Presentation.Identity
{
    public sealed class UserContext(HttpContext httpContext)
        : IUserContext
    {
        public Guid Id =>
            httpContext
                .User
                .GetUserId();
        public string Email =>
            httpContext
                .User
                .GetUserEmail();
    }
}
