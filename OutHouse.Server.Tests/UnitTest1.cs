using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using OutHouse.Server.Users;

namespace OutHouse.Server.Tests
{
    public class Tests
    {

        DbContextOptions<UserContext> _options;
        UserContext _context;
        ILogger<UserController> _logger;
        
        [OneTimeSetUp]
        public void Setup()
        {
             _options = new DbContextOptionsBuilder<UserContext>()
                .UseInMemoryDatabase("BloggingControllerTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new UserContext(_options);
            _logger = new NullLogger<UserController>();

            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.AddRange(
                   new User { UserId = 1, Name = "Pete" },
                   new User { UserId = 2, Name = "Carl" }
                );
            _context.SaveChanges();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task GetUsers()
        {
            UserController controller = new(_logger, _context);
            IEnumerable<User> users = await controller.GetUsers();
            users.Should().HaveCount(2);
        }
    }
}