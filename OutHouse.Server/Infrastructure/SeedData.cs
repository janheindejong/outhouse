using Microsoft.AspNetCore.Identity;
using OutHouse.Server.Domain;
using OutHouse.Server.Models;

namespace OutHouse.Server.Infrastructure
{
    public static class SeedData
    {

        public static List<User> Users { get; } = [
                CreateUser("2164d31d-b9ce-4a89-8737-c187dfacee09", "owner@outhouse.com", "Test123!", "35a6c895-a2b9-4353-a9db-e0b7bf0816cf"),
                CreateUser("67fc65f0-e6bb-461c-8f35-b57e280ac5b6", "admin@outhouse.com", "Test123!", "cedb285f-cc71-4ffe-a1fe-21847c131aae"),
                CreateUser("4efeaa82-2c96-4d99-ba7c-bce6b3901f26", "member@outhouse.com", "Test123!", "978213ec-e24b-47ad-900c-c08a759bc25c"),
                CreateUser("a6050009-75b2-48a0-a591-5759f3065af3", "guest@outhouse.com", "Test123!", "9ecbc49e-438b-45a0-b804-594cd8822bf3")
                ];

        public static List<Outhouse> Outhouses { get; } = [
                new Outhouse
                {
                    Id = new("acdd236c-e699-434b-9024-48e614b1ae58"),
                    Name = "My Outhouse"
                }
                ];

        public static List<Member> Members { get; } =
        [
            new Member
            {
                Id = new("54f1504a-5e01-4d89-9446-4639762e13cc"),
                Email = "owner@outhouse.com",
                Role = Role.Owner,
                Name="Owner",
                OuthouseId = new("acdd236c-e699-434b-9024-48e614b1ae58")
            },
            new Member
            {
                Id = new("681198e0-2671-455e-a759-e532916f50dc"),
                Email = "admin@outhouse.com",
                Role = Role.Admin,
                Name="Admin",
                OuthouseId = new("acdd236c-e699-434b-9024-48e614b1ae58")
            },
            new Member
            {
                Id = new("275e4646-2730-4656-9fe6-9ff80069cb1b"),
                Email = "member@outhouse.com",
                Role = Role.Member,
                Name="Member",
                OuthouseId = new("acdd236c-e699-434b-9024-48e614b1ae58")
            },
        ];

        private static User CreateUser(string id, string email, string password, string securityStamp)
        {
            User user = new()
            {
                Id = new Guid(id),
                UserName = email,
                NormalizedUserName = email.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = securityStamp,
            };

            var hasher = new PasswordHasher<User>();
            var passwordHash = hasher.HashPassword(user, password);
            user.PasswordHash = passwordHash;
            return user;
        }
    }
}
