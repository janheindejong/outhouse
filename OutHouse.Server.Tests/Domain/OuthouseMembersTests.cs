using FluentAssertions;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Domain.Members;
using OutHouse.Server.Models;

namespace OutHouse.Server.Tests.Domain
{
    public class OuthouseMembersTests
    {

        private record struct MemberData(string Email, string Name, Guid Id);

        private static readonly MemberData owner = new("owner@outhouse.com", "Owner", Guid.NewGuid());
        private static readonly MemberData admin = new("admin@outhouse.com", "Admin", Guid.NewGuid());
        private static readonly MemberData member = new("member@outhouse.com", "Member", Guid.NewGuid());
        private static readonly MemberData guest = new("guest@outhouse.com", "Guest", Guid.NewGuid());

        [Test]
        public void AddMember()
        {
            Outhouse outhouse = GetOuthouse();
            Member member = outhouse.AddMember(guest.Email, guest.Name, Role.Member);
            outhouse.Members.Count.Should().Be(4);
            member.Email.Should().Be(guest.Email);
            member.Name.Should().Be(guest.Name);
        }

        [Test]
        public void AddMember_AlreadyExisting_ThrowsException()
        {
            // Arrange
            Outhouse outhouse = GetOuthouse();
            Func<Member> act = () => outhouse.AddMember(member.Email, member.Name, Role.Member);
            act.Should().Throw<SeeOtherException>();
        }

        [Test]
        public void DeleteMember()
        {
            Outhouse outhouse = GetOuthouse();
            Member member = outhouse.DeleteMember(admin.Id);
            member.Email.Should().Be(admin.Email);
            member.Name.Should().Be(admin.Name);
            outhouse.Members.Count.Should().Be(2);
        }

        [Test]
        public void DeleteMember_Owner_ThrowsException()
        {
            Outhouse outhouse = GetOuthouse();
            Func<Member> act = () => outhouse.DeleteMember(owner.Id);
            act.Should().Throw<ConflictException>();
        }

        [Test]
        public void ModifyMemberRole_Owner_ThrowsException()
        {
            Outhouse outhouse = GetOuthouse();
            Func<Member> act = () => outhouse.ModifyMemberRole(owner.Id, Role.Admin);
            act.Should().Throw<ConflictException>();
        }

        [TestCase("owner@outhouse.com", true)]
        [TestCase("admin@outhouse.com", false)]
        [TestCase("member@outhouse.com", false)]
        [TestCase("guest@outhouse.com", false)]
        public void HasOwner(string email, bool expected)
        {
            Outhouse outhouse = GetOuthouse();
            bool result = outhouse.HasOwner(email);
            result.Should().Be(expected);
        }

        [TestCase("owner@outhouse.com", true)]
        [TestCase("admin@outhouse.com", true)]
        [TestCase("member@outhouse.com", false)]
        [TestCase("guest@outhouse.com", false)]
        public void HasAdmin(string email, bool expected)
        {
            Outhouse outhouse = GetOuthouse();
            bool result = outhouse.HasAdmin(email);
            result.Should().Be(expected);
        }

        [TestCase("owner@outhouse.com", true)]
        [TestCase("admin@outhouse.com", true)]
        [TestCase("member@outhouse.com", true)]
        [TestCase("guest@outhouse.com", false)]
        public void HasMember(string email, bool expected)
        {
            Outhouse outhouse = GetOuthouse();
            bool result = outhouse.HasMember(email);
            result.Should().Be(expected);
        }



        private static Outhouse GetOuthouse()
        {
            Outhouse outhouse = new();
            outhouse.Members.Add(
                new Member()
                {
                    Id = owner.Id,
                    Email = owner.Email,
                    Name = owner.Name,
                    Role = Role.Owner,
                });
            outhouse.Members.Add(
                new Member()
                {
                    Id = admin.Id,
                    Email = admin.Email,
                    Name = admin.Name,
                    Role = Role.Admin,
                });
            outhouse.Members.Add(
                new Member()
                {
                    Id = member.Id,
                    Email = member.Email,
                    Name = member.Name,
                    Role = Role.Member,
                });
            return outhouse;
        }
    }
}
