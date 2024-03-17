using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutHouse.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"), 0, "8822ee1c-0b75-48cb-bd95-460892b5dc40", "owner@outhouse.com", false, false, null, "OWNER@OUTHOUSE.COM", "OWNER@OUTHOUSE.COM", "AQAAAAIAAYagAAAAEMxoXaKy0Bud0StZFPPf5mXNbEL7WJMLP0IdOIqf2PD3etF4XSMBF8YnrBMHKV8Jrw==", null, false, "35a6c895-a2b9-4353-a9db-e0b7bf0816cf", false, "owner@outhouse.com" },
                    { new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"), 0, "77daa6dd-1b87-4c69-a426-f4f68dfd8032", "member@outhouse.com", false, false, null, "MEMBER@OUTHOUSE.COM", "MEMBER@OUTHOUSE.COM", "AQAAAAIAAYagAAAAEE91UXhvl3iqR69ltrqEARNQwxSLUj3lWZyuKm0K+/AmY8BgCL0x89pvxPRsm6iJuw==", null, false, "978213ec-e24b-47ad-900c-c08a759bc25c", false, "member@outhouse.com" },
                    { new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"), 0, "0fa8b915-5411-4d64-bebe-715ce79f0e34", "admin@outhouse.com", false, false, null, "ADMIN@OUTHOUSE.COM", "ADMIN@OUTHOUSE.COM", "AQAAAAIAAYagAAAAEEC1MX4h+By92E/wQTpaL1eIeiJVcE9pDdUgzWwzHG8xveJHGqwMBqM96Pio4X86ug==", null, false, "cedb285f-cc71-4ffe-a1fe-21847c131aae", false, "admin@outhouse.com" },
                    { new Guid("a6050009-75b2-48a0-a591-5759f3065af3"), 0, "785ae725-50c2-400a-aa27-5caadf216622", "guest@outhouse.com", false, false, null, "GUEST@OUTHOUSE.COM", "GUEST@OUTHOUSE.COM", "AQAAAAIAAYagAAAAEJsjrprtW9aTZ6x0meULdeUI4U9gIwJNsNQcl6WnEwrv0X11GcqPMv+V12KaSkCZMg==", null, false, "9ecbc49e-438b-45a0-b804-594cd8822bf3", false, "guest@outhouse.com" }
                });

            migrationBuilder.InsertData(
                table: "Outhouses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), "My Outhouse" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "Name", "OuthouseId", "Role" },
                values: new object[,]
                {
                    { new Guid("275e4646-2730-4656-9fe6-9ff80069cb1b"), "member@outhouse.com", "Member", new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), 2 },
                    { new Guid("54f1504a-5e01-4d89-9446-4639762e13cc"), "owner@outhouse.com", "Owner", new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), 0 },
                    { new Guid("681198e0-2671-455e-a759-e532916f50dc"), "admin@outhouse.com", "Admin", new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6050009-75b2-48a0-a591-5759f3065af3"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("275e4646-2730-4656-9fe6-9ff80069cb1b"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("54f1504a-5e01-4d89-9446-4639762e13cc"));

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: new Guid("681198e0-2671-455e-a759-e532916f50dc"));

            migrationBuilder.DeleteData(
                table: "Outhouses",
                keyColumn: "Id",
                keyValue: new Guid("acdd236c-e699-434b-9024-48e614b1ae58"));
        }
    }
}
