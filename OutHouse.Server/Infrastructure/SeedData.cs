using Microsoft.AspNetCore.Identity;
using OutHouse.Server.Domain;
using OutHouse.Server.Models;
using System.Net.NetworkInformation;

namespace OutHouse.Server.Infrastructure
{
    public static class SeedData
    {

        public static List<User> Users { get; } = 
        [
            CreateUser("2164d31d-b9ce-4a89-8737-c187dfacee09", "owner@outhouse.com", "Test123!", "35a6c895-a2b9-4353-a9db-e0b7bf0816cf"),
            CreateUser("67fc65f0-e6bb-461c-8f35-b57e280ac5b6", "admin@outhouse.com", "Test123!", "cedb285f-cc71-4ffe-a1fe-21847c131aae"),
            CreateUser("4efeaa82-2c96-4d99-ba7c-bce6b3901f26", "member@outhouse.com", "Test123!", "978213ec-e24b-47ad-900c-c08a759bc25c"),
            CreateUser("a6050009-75b2-48a0-a591-5759f3065af3", "guest@outhouse.com", "Test123!", "9ecbc49e-438b-45a0-b804-594cd8822bf3")
        ];

        public static List<Outhouse> Outhouses { get; } = 
        [
            CreateOuthouse("acdd236c-e699-434b-9024-48e614b1ae58", "My Outhouse"),
            CreateOuthouse("008f84df-f856-4a4d-b92b-314cdecd6fab", "Orphan Outhouse")
        ];

        public static List<Member> Members { get; } =
        [
            CreateMember("54f1504a-5e01-4d89-9446-4639762e13cc", "owner@outhouse.com", Role.Owner, "Owner", "acdd236c-e699-434b-9024-48e614b1ae58"),
            CreateMember("681198e0-2671-455e-a759-e532916f50dc", "admin@outhouse.com", Role.Admin, "Admin", "acdd236c-e699-434b-9024-48e614b1ae58"),
            CreateMember("275e4646-2730-4656-9fe6-9ff80069cb1b", "member@outhouse.com", Role.Member, "Member", "acdd236c-e699-434b-9024-48e614b1ae58")
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

        private static Outhouse CreateOuthouse(string id, string name)
            => new() { Id = new Guid(id), Name = name };

        private static Member CreateMember(string id, string email, Role role, string name, string outhouseId)
            => new() { Id = new Guid(id), Email = email, Role = role, Name = name, OuthouseId = new Guid(outhouseId) };
    }
}
