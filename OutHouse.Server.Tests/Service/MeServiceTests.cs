using FluentAssertions;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;

namespace OutHouse.Server.Tests.Service
{
    public class MeServiceTests : MeServiceTestsBase
    {
        [Test]
        public async Task GetOuthouses_Owner_ReturnsOneAsync()
        {
            ApplicationDbContext context = GetDbContext();
            MeService service = new(context, OwnerContext);

            List<OuthouseDto> result = await service.GetOuthousesAsync();

            result.Should().HaveCount(1);
        }

        [Test]
        public async Task GetOuthouses_Guest_ReturnsNoneAsync()
        {
            ApplicationDbContext context = GetDbContext();
            MeService service = new(context, GuestContext);

            List<OuthouseDto> result = await service.GetOuthousesAsync();

            result.Should().BeEmpty();
        }
    }
}
