using FluentAssertions;
using FluentAssertions.Execution;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Infrastructure;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;

namespace OutHouse.Server.Tests.Service
{
    internal class OuthouseMemberServiceTests : ServiceTestBase
    {

        [Test]
        public async Task GetMembers()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseMemberService service = new(dbContext, MemberContext);
            List<MemberDto> result = await service.GetMembersAsync(OuthouseId);
            result.Should().HaveCount(3);
        }

        [Test]
        public async Task AddMember()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();
            OuthouseMemberService service = new(dbContext, AdminContext);
            AddMemberRequest request = new("guest@outhouse.com", "Guest", Role.Member);
            MemberDto result = await service.AddMemberAsync(OuthouseId, request);
            List<MemberDto> members = await service.GetMembersAsync(OuthouseId);
            using (new AssertionScope())
            {
                result.Name.Should().Be(request.MemberName);
                result.Email.Should().Be(request.MemberEmail);
                result.Role.Should().Be("Member");
                members.Count.Should().Be(4);
            }

            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task AddMember_NonAdmin_RaisesForbidden()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseMemberService service = new(dbContext, MemberContext);
            AddMemberRequest request = new("guest@outhouse.com", "Guest", Role.Member);
            Func<Task<MemberDto>> act = () => service.AddMemberAsync(OuthouseId, request);
            await act.Should().ThrowAsync<ForbiddenException>();
        }

        [Test]
        public async Task RemoveMember()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            dbContext.Database.BeginTransaction();
            OuthouseMemberService service = new(dbContext, AdminContext);
            MemberDto result = await service.RemoveMemberAsync(OuthouseId, new("275e4646-2730-4656-9fe6-9ff80069cb1b"));
            List<MemberDto> members = await service.GetMembersAsync(OuthouseId);
            using (new AssertionScope())
            {
                result.Name.Should().Be("Member");
                result.Email.Should().Be("member@outhouse.com");
                result.Role.Should().Be("Member");
                members.Count.Should().Be(2);
            }

            dbContext.ChangeTracker.Clear();
        }

        [Test]
        public async Task RemoveMember_NonAdmin_RaisesForbidden()
        {
            ApplicationDbContext dbContext = CreateDbContext();
            OuthouseMemberService service = new(dbContext, MemberContext);
            Func<Task<MemberDto>> act = () => service.RemoveMemberAsync(OuthouseId, new("275e4646-2730-4656-9fe6-9ff80069cb1b"));
            await act.Should().ThrowAsync<ForbiddenException>();
        }
    }
}
