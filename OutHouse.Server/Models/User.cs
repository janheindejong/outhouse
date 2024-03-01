using Microsoft.AspNetCore.Identity;

namespace OutHouse.Server.Models
{
    public class User : IdentityUser<Guid>, IUser
    {
    }
}
