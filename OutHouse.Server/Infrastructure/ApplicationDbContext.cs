using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain;
using OutHouse.Server.Domain.Bookings;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Models;
using OutHouse.Server.Service;

namespace OutHouse.Server.Infrastructure
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options), IDbContext
    {
        
        public DbSet<Outhouse> Outhouses { get; set; }
        
        public DbSet<Member> Members { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>().HasData(SeedData.Users);
            builder.Entity<Outhouse>().HasData(SeedData.Outhouses);
            builder.Entity<Member>().HasData(SeedData.Members);
            builder.Entity<Booking>().HasData(SeedData.Bookings);
        }
    }
}
