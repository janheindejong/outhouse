using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain;
using OutHouse.Server.Models;
using OutHouse.Server.Service;

namespace OutHouse.Server.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IDbContext
    {
        public DbSet<Outhouse> Outhouses { get; set; }
        public DbSet<Member> Members { get; set; }
    }
}
