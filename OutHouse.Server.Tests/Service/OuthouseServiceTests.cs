using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Models;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;

namespace OutHouse.Server.Tests.Service
{
    internal class OuthouseServiceTests : ServiceTestBase
    {

        [Test]
        public async Task GetOuthouseByIdAsync()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseService service = new(dbContext, MemberContext);

            OuthouseDto result = await service.GetOuthouseByIdAsync(OuthouseId);

            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetOuthouseByIdAsync_Guest_RaisesException()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseService service = new(dbContext, GuestContext);

            Func<Task<OuthouseDto>> act = () => service.GetOuthouseByIdAsync(OuthouseId);

            await act.Should().ThrowAsync<ForbiddenException>();
        }

        [Test]
        public async Task CreateNewOuthouseAsync()
        {
            ApplicationDbContext dbContext = CreateDbContext();

            dbContext.Database.BeginTransaction();

            OuthouseService service = new(dbContext, GuestContext);

            CreateNewOuthouseRequest request = new()
            {
                Name = "New Outhouse",
                OwnerName = "Guest"
            };

            OuthouseDto result = await service.CreateNewOuthouseAsync(request);

            Outhouse outhouse = dbContext.Outhouses.Where(x => x.Id == result.Id).Single();
            Member member = outhouse.Members.Single();

            using (new AssertionScope())
            {
                dbContext.Entry(outhouse).State.Should().Be(EntityState.Unchanged);
                outhouse?.Name.Should().Be("New Outhouse");
                member?.Name.Should().Be("Guest");
                member?.Email.Should().Be(GuestContext.Email);
                member?.Role.Should().Be(Role.Owner);
            }

            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task RemoveOuthouseAsync()
        {
            ApplicationDbContext dbContext = CreateDbContext();

            dbContext.Database.BeginTransaction();

            OuthouseService service = new(dbContext, OwnerContext);

            OuthouseDto result = await service.RemoveOuthouseAsync(OuthouseId);

            using (new AssertionScope())
            {
                result?.Id.Should().Be(OuthouseId);
                dbContext.Outhouses.Should().HaveCount(1);
            }

            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task RemoveOuthouseAsync_Admin_ShouldThrowForbiddenException()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseService service = new(dbContext, AdminContext);

            Func<Task<OuthouseDto>> act = () => service.RemoveOuthouseAsync(OuthouseId);

            using (new AssertionScope())
            {
                await act.Should().ThrowAsync<ForbiddenException>();
                dbContext.Outhouses.Should().HaveCount(2);
            }
        }
    }
}
