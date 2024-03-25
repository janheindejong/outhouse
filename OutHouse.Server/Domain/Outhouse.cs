using OutHouse.Server.Domain.Members;

namespace OutHouse.Server.Models
{
    public partial class Outhouse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

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

    }
}
