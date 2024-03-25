using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service;

namespace OutHouse.Server.Tests.Service
{
    public class ServiceTestBase
    {

        const string connectionString =
                    "Server=localhost,1433;" +
                    "Database=OuthouseDbTest;" +
                    "User ID=sa;" +
                    "Password=yourStrong(!)Password;" +
                    "Persist Security Info=False;" +
                    "TrustServerCertificate=true;";

        private DbContextOptions<ApplicationDbContext> DbContextOptions { get; set; }

        protected UserContext OwnerContext { get; } = new("owner@outhouse.com");

        protected UserContext AdminContext { get; } = new("admin@outhouse.com");

        protected UserContext MemberContext { get; } = new("member@outhouse.com");

        protected UserContext GuestContext { get; } = new("guest@outhouse.com");

        protected static Guid OuthouseId => new("acdd236c-e699-434b-9024-48e614b1ae58");

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            DbContextOptionsBuilder<ApplicationDbContext> optionsBldr = new();
            optionsBldr.UseSqlServer(connectionString);
            DbContextOptions = optionsBldr.Options;
            ApplicationDbContext context = CreateDbContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        protected ApplicationDbContext CreateDbContext() => new(DbContextOptions);

        protected record class UserContext(string Email) : IUserContext;
    }
}
