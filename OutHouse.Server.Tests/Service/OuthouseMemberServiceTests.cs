using FluentAssertions;
using FluentAssertions.Execution;
using OutHouse.Server.Domain.Bookings;
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
            
            dbContext.ChangeTracker.Clear();
            
            List<MemberDto> members = await service.GetMembersAsync(OuthouseId);
            using (new AssertionScope())
            {
                result.Name.Should().Be(request.MemberName);
                result.Email.Should().Be(request.MemberEmail);
                result.Role.Should().Be("Member");
                members.Count.Should().Be(4);
            }

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

            Guid memberId = new("275e4646-2730-4656-9fe6-9ff80069cb1b");
            OuthouseMemberService service = new(dbContext, AdminContext);

            await service.RemoveMemberAsync(OuthouseId, memberId);
            
            List<MemberDto> members = await service.GetMembersAsync(OuthouseId);
            
            dbContext.ChangeTracker.Clear();

            Booking? member = dbContext.Bookings.Where(x => x.Id == memberId).SingleOrDefault();
            member.Should().BeNull();
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
