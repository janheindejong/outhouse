using OutHouse.Server.Models;

namespace OutHouse.Server.Domain.Members
{
    public class Member
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public Guid OuthouseId { get; set; }

        public Outhouse Outhouse { get; set; } = null!;

        public Role Role { get; set; }

        public bool HasOwnerPrivileges => Role == Role.Owner;

        public bool HasAdminPrivileges => Role == Role.Owner || Role == Role.Admin;
    }

    public enum Role { Owner, Admin, Member }
}
