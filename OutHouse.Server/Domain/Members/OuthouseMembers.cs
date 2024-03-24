using System.Data;
using OutHouse.Server.Domain.Exceptions;
using OutHouse.Server.Domain.Members;

namespace OutHouse.Server.Models
{
    public partial class Outhouse
    {

        public ICollection<Member> Members { get; } = [];

        public Member AddMember(string email, string name, Role role)
        {
            if (HasMember(email))
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


        public Member? GetMemberByEmail(string email) { 
            return Members.Where(x => x.Email == email).SingleOrDefault();
        }

        public bool HasOwner(string email)
        {
            Member? member = GetMemberByEmail(email);
            return member != null && member.HasOwnerPrivileges;
        }

        public bool HasAdmin(string email)
        {
            Member? member = GetMemberByEmail(email);
            return member != null && member.HasAdminPrivileges;
        }

        public bool HasMember(string email)
        {
            return GetMemberByEmail(email) != null;
        }
    }
}
