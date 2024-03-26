using FluentAssertions;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;

namespace OutHouse.Server.Tests.Service
{
    public class MeServiceTests : ServiceTestBase
    {
        [Test]
        public async Task GetOuthouses()
        {
            ApplicationDbContext context = CreateDbContext();
            MeService service = new(context, OwnerContext);

            List<OuthouseDto> result = await service.GetOuthousesAsync();

            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetOuthouses_Guest_ReturnsNone()
        {
            ApplicationDbContext context = CreateDbContext();
            MeService service = new(context, GuestContext);

            List<OuthouseDto> result = await service.GetOuthousesAsync();

            result.Should().BeEmpty();
        }

        [Test]
        public async Task GetBookings()
        {
            ApplicationDbContext context = CreateDbContext();
            MeService service = new(context, MemberContext);

            List<BookingDto> result = await service.GetBookingsAsync();

            result.Should().HaveCount(5);
        }

        [Test]
        public async Task GetBookings_Guest_ReturnsNone()
        {
            ApplicationDbContext context = CreateDbContext();
            MeService service = new(context, GuestContext);

            List<BookingDto> result = await service.GetBookingsAsync();

            result.Should().BeEmpty();
        }
    }
}
