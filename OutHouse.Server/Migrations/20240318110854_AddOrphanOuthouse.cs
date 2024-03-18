using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutHouse.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddOrphanOuthouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b327db39-e506-4e1e-9761-07fd6c833c07", "AQAAAAIAAYagAAAAEBECqckpJcamcjLxhTikBUbIYVwmiRGForlsUR68dkrD7HUGkElUZYpDLJla5dQXYA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "430f3ca5-23be-41ea-b474-f102bbe72002", "AQAAAAIAAYagAAAAECMFccanwNLdVlOPQRpWfJdKRTPZSUGsJ+MYS/gUh+azJDl+PFjbXxRHV2o+OJFJgg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4fdf832a-11e4-4fc2-97e5-2779bb7da145", "AQAAAAIAAYagAAAAEC0CCMf/b0w7MR/tq1/GjevJOfKzONaWO6QMXqZgtss7mEoiimqQEeotsvYKlJQCpw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6050009-75b2-48a0-a591-5759f3065af3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "904d7340-39dc-4b66-803c-8518c7db226b", "AQAAAAIAAYagAAAAEJgr35akxGgpVRjexBaa94Rq4LXc6wX0Z8VruBIXmn5DWqXtJFHIeFvLgVa8huy8sw==" });

            migrationBuilder.InsertData(
                table: "Outhouses",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("008f84df-f856-4a4d-b92b-314cdecd6fab"), "Orphan Outhouse" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Outhouses",
                keyColumn: "Id",
                keyValue: new Guid("008f84df-f856-4a4d-b92b-314cdecd6fab"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8822ee1c-0b75-48cb-bd95-460892b5dc40", "AQAAAAIAAYagAAAAEMxoXaKy0Bud0StZFPPf5mXNbEL7WJMLP0IdOIqf2PD3etF4XSMBF8YnrBMHKV8Jrw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "77daa6dd-1b87-4c69-a426-f4f68dfd8032", "AQAAAAIAAYagAAAAEE91UXhvl3iqR69ltrqEARNQwxSLUj3lWZyuKm0K+/AmY8BgCL0x89pvxPRsm6iJuw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0fa8b915-5411-4d64-bebe-715ce79f0e34", "AQAAAAIAAYagAAAAEEC1MX4h+By92E/wQTpaL1eIeiJVcE9pDdUgzWwzHG8xveJHGqwMBqM96Pio4X86ug==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6050009-75b2-48a0-a591-5759f3065af3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "785ae725-50c2-400a-aa27-5caadf216622", "AQAAAAIAAYagAAAAEJsjrprtW9aTZ6x0meULdeUI4U9gIwJNsNQcl6WnEwrv0X11GcqPMv+V12KaSkCZMg==" });
        }
    }
}
