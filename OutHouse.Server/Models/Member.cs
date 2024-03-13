using OutHouse.Server.Identity;

namespace OutHouse.Server.Models
{
    public class Member
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public Guid OuthouseId { get; set; }

        public Outhouse Outhouse { get; set; } = null!; 

        public Role Role { get; set; }
    }

    public enum Role { Admin, Member }
}
