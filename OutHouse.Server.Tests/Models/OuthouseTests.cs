using FluentAssertions;
using OutHouse.Server.Models;

namespace OutHouse.Server.Tests.Models
{
    public class OuthouseTests
    {

        private static readonly UserStub adminUser = new(Guid.NewGuid());
        private static readonly UserStub memberUser = new(Guid.NewGuid());
        private static readonly UserStub nonMemberUser = new(Guid.NewGuid());

        [Test]
        public void AddMember_ReturnsSuccess()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.AddMember(nonMemberUser, Role.Member, adminUser);
            result.IsSuccess.Should().BeTrue();
            outhouse.Members.Count.Should().Be(3);
        }

        [Test]
        public void AddMember_AlreadyExisting_ReturnsFailure()
        {
            // Arrange
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.AddMember(memberUser, Role.Member, adminUser);
            result.IsSuccess.Should().BeFalse();
            outhouse.Members.Count.Should().Be(2);
        }

        [Test]
        public void AddMember_NonAdmin_ReturnsFailure()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.AddMember(nonMemberUser, Role.Member, memberUser);
            result.IsSuccess.Should().BeFalse();
            outhouse.Members.Count.Should().Be(2);
        }

        [Test]
        public void ModifyMemberRole_ReturnsSuccess()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.ModifyMemberRole(memberUser, Role.Admin, adminUser);
            result.IsSuccess.Should().BeTrue();
            outhouse.Members.Count.Should().Be(2);
            outhouse.Members.Where(x => x.Role == Role.Admin).Count().Should().Be(2);
        }

        [Test]
        public void DeleteMember_ReturnsSuccess()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.DeleteMember(memberUser, adminUser);
            result.IsSuccess.Should().BeTrue();
            outhouse.Members.Count.Should().Be(1);
        }

        [Test]
        public void DeleteMember_SameUser_ReturnsSuccess()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.DeleteMember(memberUser, memberUser);
            result.IsSuccess.Should().BeTrue();
            outhouse.Members.Count.Should().Be(1);
        }

        [Test]
        public void DeleteMember_LastAdmin_ReturnsFailure()
        {
            Outhouse outhouse = GetOuthouse();
            Result result = outhouse.DeleteMember(adminUser, adminUser);
            result.IsSuccess.Should().BeFalse();
            outhouse.Members.Count.Should().Be(2);
        }

        private static Outhouse GetOuthouse()
        {
            Outhouse outhouse = new();
            outhouse.Members.Add(
                new Member()
                {
                    Id = Guid.NewGuid(),
                    UserId = adminUser.Id,
                    Role = Role.Admin,
                });
            outhouse.Members.Add(
                new Member()
                {
                    Id = Guid.NewGuid(),
                    UserId = memberUser.Id,
                    Role = Role.Member,
                });
            return outhouse;
        }

        private record class UserStub(Guid Id) : IUser;
    }
}
