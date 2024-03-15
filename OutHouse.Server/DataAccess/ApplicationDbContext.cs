using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain;
using OutHouse.Server.Models;

namespace OutHouse.Server.DataAccess
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        public DbSet<Outhouse> Outhouses { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
