using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Models;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Tests.Domain;

namespace OutHouse.Server.Tests.Service
{
    internal class OuthouseServiceTests : MeServiceTestsBase
    {

        private readonly Guid OuthouseId = new("acdd236c-e699-434b-9024-48e614b1ae58");

        [Test]
        public async Task GetOuthouseByIdAsync()
        {
            ApplicationDbContext dbContext = GetDbContext();
            OuthouseService service = new(dbContext, MemberContext);

            OuthouseDto result = await service.GetOuthouseByIdAsync(OuthouseId);

            result.Should().NotBeNull();
        }

        [Test]
        public async Task GetOuthouseByIdAsync_Guest_RaisesException()
        {
            ApplicationDbContext dbContext = GetDbContext();
            OuthouseService service = new(dbContext, GuestContext);

            Func<Task<OuthouseDto>> act = () => service.GetOuthouseByIdAsync(OuthouseId);

            await act.Should().ThrowAsync<ForbiddenException>();
        }

        [Test]
        public async Task CreateNewOuthouseAsync()
        {
            ApplicationDbContext dbContext = GetDbContext();
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
        }

        [Test]
        public async Task RemoveOuthouseAsync()
        {
            ApplicationDbContext dbContext = GetDbContext();
            OuthouseService service = new(dbContext, OwnerContext);

            OuthouseDto result = await service.RemoveOuthouseAsync(OuthouseId);

            using (new AssertionScope())
            {
                result?.Id.Should().Be(OuthouseId);
                dbContext.Outhouses.Should().HaveCount(1);
            }
        }

        [Test]
        public async Task RemoveOuthouseAsync_Admin_ShouldThrowForbiddenException()
        {
            ApplicationDbContext dbContext = GetDbContext();
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
