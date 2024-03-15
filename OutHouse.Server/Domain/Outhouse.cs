using System.Data;
using OutHouse.Server.Domain.Exceptions;

namespace OutHouse.Server.Models
{
    public class Outhouse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Member> Members { get; } = [];

        public Member AddMember(string email, string name, Role role)
        {
            if (IsMember(email))
            {
                Guid id = Members.Where(x => x.Email == email).First().Id;
                throw new SeeOtherException("AddMember", "Member", id);
            }

            Member newMember = new()
            {
                Email = email,
                Name = name,
                OuthouseId = Id,
                Role = role
            };
            Members.Add(newMember);
            return newMember;
        }

        public Member DeleteMember(Guid id)
        {
            Member? member = Members.Where(x => x.Id == id).FirstOrDefault() 
                ?? throw new NotFoundException("Member", id);
            if (member.Role == Role.Owner)
            {
                throw new NotAllowedException("delete", "Member", id, "Can't delete owner");
            }

            Members.Remove(member);
            return member;
        }

        public Member ModifyMemberRole(Guid memberId, Role newRole)
        {
            Member? member = Members.Where(x => x.Id == memberId).FirstOrDefault() 
                ?? throw new NotFoundException("Member", memberId);
            if (member.Role == Role.Owner && newRole != Role.Owner)
            {
                throw new NotAllowedException("delete", "Member", memberId, "Can't modify owner role");
            }

            member.Role = newRole;
            return member;
        }

        public void ModifyMemberName(Guid memberId, string newName)
        {
            Member? member = Members.Where(x => x.Id == memberId).FirstOrDefault() 
                ?? throw new NotFoundException("Member", memberId);
            member.Name = newName;
        }

        public static Outhouse CreateNew(string name, string ownerEmail, string ownerName)
        {
            Outhouse outhouse = new()
            {
                Name = name
            };

            Member owner = new()
            {
                Email = ownerEmail,
                OuthouseId = outhouse.Id,
                Name = ownerName,
                Role = Role.Owner
            };

            outhouse.Members.Add(owner);
            return outhouse;
        }

        public bool HasOwnerPrivileges(string userEmail)
        {
            return Members.Any(x => x.Email == userEmail && x.Role == Role.Owner);
        }

        public bool HasAdminPrivileges(string userEmail)
        {
            return Members.Any(x => x.Email == userEmail && (x.Role == Role.Admin || x.Role == Role.Owner));
        }

        public bool IsMember(string userEmail)
        {
            return Members.Any(x => x.Email == userEmail);
        }
    }
}
