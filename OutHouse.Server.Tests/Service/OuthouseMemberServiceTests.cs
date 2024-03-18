using FluentAssertions;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Tests.Domain;

namespace OutHouse.Server.Tests.Service
{
    internal class OuthouseMemberServiceTests : MeServiceTestsBase
    {

        private readonly Guid OuthouseId = new("acdd236c-e699-434b-9024-48e614b1ae58");

        [Test]
        public async Task GetMembers()
        {
            ApplicationDbContext dbContext = GetDbContext();
            OuthouseMemberService service = new(dbContext, MemberContext, OuthouseId);

            List<MemberDto> result = await service.GetMembersAsync();

            result.Should().HaveCount(3);
        }
    }
}
