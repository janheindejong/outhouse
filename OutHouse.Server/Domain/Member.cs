namespace OutHouse.Server.Models
{
    public class Member
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty; 

        public string Email { get; set; } = string.Empty;

        public Guid OuthouseId { get; set; }

        public Outhouse Outhouse { get; set; } = null!;

        public Role Role { get; set; }
    } 

    public enum Role { Owner, Admin, Member }
}
