using OutHouse.Server.Handlers;
using OutHouse.Server.Models;

namespace OutHouse.Server.Presentation.Identity
{
    public sealed class UserContext(HttpContext httpContext)
        : IUserContext
    {
        public string Email =>
            httpContext
                .User
                .Identity?
                .Name ??
            throw new ApplicationException("E-mail is unavailable");

        public Guid Id =>
            httpContext
                .User
                .GetUserId();
    }
}
