using Microsoft.AspNetCore.Identity;
using OutHouse.Server.Domain;
using OutHouse.Server.Models;

namespace OutHouse.Server.Infrastructure
{
    public static class SeedData
    {

        private static readonly Guid outhouseId = Guid.Parse("acdd236c-e699-434b-9024-48e614b1ae58");

        private static readonly List<User> users = [
                CreateUser("owner@outhouse.com", "Test123!"),
                CreateUser("admin@outhouse.com", "Test123!"),
                CreateUser("member@outhouse.com", "Test123!"),
                CreateUser("guest@outhouse.com", "Test123!")
                ];

        private static readonly List<Outhouse> outhouses = [
                new Outhouse { Id = outhouseId, Name = "My Outhouse" }
                ];

        private static readonly List<Member> members = [
                new Member { Id = Guid.NewGuid(), Email = "owner@outhouse.com", Role = Role.Owner, Name="Owner", OuthouseId = outhouseId },
                new Member { Id = Guid.NewGuid(), Email = "admin@outhouse.com", Role = Role.Admin, Name="Admin", OuthouseId = outhouseId },
                new Member { Id = Guid.NewGuid(), Email = "member@outhouse.com", Role = Role.Member, Name="Member", OuthouseId = outhouseId },
                ];

        public static List<User> Users => users;

        public static List<Outhouse> Outhouses => outhouses;

        public static List<Member> Members => members; 

        private static User CreateUser(string email, string password)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(user, password);
            user.PasswordHash = passwordHash;
            return user;
        }
    }
}
