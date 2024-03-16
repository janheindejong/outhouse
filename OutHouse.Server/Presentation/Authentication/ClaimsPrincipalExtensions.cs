using System.Security.Claims;

namespace OutHouse.Server.Presentation.Identity
{
    internal static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal? principal)
        {
            string? userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(userId, out Guid parsedUserId) ?
                parsedUserId :
                throw new ApplicationException("User id is unavailable");
        }

        public static string GetUserEmail(this ClaimsPrincipal? principal)
        {
            return principal?.FindFirstValue(ClaimTypes.Email) 
                ?? throw new ApplicationException("User email is unavailable");
        }
    }
}
