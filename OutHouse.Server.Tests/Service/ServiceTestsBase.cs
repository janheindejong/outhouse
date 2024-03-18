using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service;

namespace OutHouse.Server.Tests.Domain
{
    public class MeServiceTestsBase
    {

        private const string ConnectionString = 
            "Server=localhost,1433;" +
            "Database=OuthouseDbTest;" +
            "User ID=sa;" +
            "Password=yourStrong(!)Password;" +
            "Persist Security Info=False;" +
            "TrustServerCertificate=true;"
            ;

        protected UserContext OwnerContext { get; } = new("owner@outhouse.com");

        protected UserContext AdminContext { get; } = new("admin@outhouse.com");

        protected UserContext MemberContext { get; } = new("member@outhouse.com");

        protected UserContext GuestContext { get; } = new("guest@outhouse.com");

        protected static ApplicationDbContext GetDbContext()
        {
            ApplicationDbContext dbContext = new(
                        new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseSqlServer(ConnectionString)
                        .Options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        protected record class UserContext(string Email) : IUserContext;

    }
}
