using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OutHouse.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddBookings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OuthouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateOnly>(type: "date", nullable: false),
                    End = table.Column<DateOnly>(type: "date", nullable: false),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Outhouses_OuthouseId",
                        column: x => x.OuthouseId,
                        principalTable: "Outhouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("2164d31d-b9ce-4a89-8737-c187dfacee09"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce28d4eb-f953-45b4-bc8c-41e60727d621", "AQAAAAIAAYagAAAAEA7koEsEHkRfALbmLBFJroYgFXSUhJPSNS+4JndjPKYslImX/2DW05xbTuIzYssXFw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4efeaa82-2c96-4d99-ba7c-bce6b3901f26"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4d1a157b-78a2-4899-92ab-26fd2bfe80d3", "AQAAAAIAAYagAAAAEIZvj6WMGfTuBrzqv7N9EumCuoEkRPrcihIts3gZrhoKjAEgX35Tr98sQSndaDPI+A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("67fc65f0-e6bb-461c-8f35-b57e280ac5b6"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fedba227-2766-448a-9604-fc8d893215b7", "AQAAAAIAAYagAAAAEHZewWWiV6qAWWsI3q9JG3+Sq5f1taKsl+qdpCcVuEJ5BErzBs+PykpOcxRS77hETQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a6050009-75b2-48a0-a591-5759f3065af3"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "137a20b6-ae7d-4fea-ac7a-077e8f24d895", "AQAAAAIAAYagAAAAEOdCYOZPJdGaOL+uWVM1PV/0w2wsCdr5oUARJj+JzxFZ3MOA3GOOKhpeewe32d/Jyg==" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookerEmail", "End", "OuthouseId", "Start", "State" },
                values: new object[,]
                {
                    { new Guid("4a241119-e25c-4d5a-9a2f-98af68abe9a3"), "member@outhouse.com", new DateOnly(2000, 1, 2), new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), new DateOnly(2000, 1, 1), 2 },
                    { new Guid("5990aea7-1c7b-48b1-8d18-de00bf98a7b5"), "member@outhouse.com", new DateOnly(2000, 1, 4), new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), new DateOnly(2000, 1, 3), 0 },
                    { new Guid("9467a1f8-56be-447e-8e96-f23510b5d7ab"), "member@outhouse.com", new DateOnly(2000, 1, 2), new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), new DateOnly(2000, 1, 1), 1 },
                    { new Guid("9628c05c-8b3c-41ca-9d53-9b111217133e"), "admin@outhouse.com", new DateOnly(2000, 1, 2), new Guid("acdd236c-e699-434b-9024-48e614b1ae58"), new DateOnly(2000, 1, 1), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OuthouseId",
                table: "Bookings",
                column: "OuthouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

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
        }
    }
}
