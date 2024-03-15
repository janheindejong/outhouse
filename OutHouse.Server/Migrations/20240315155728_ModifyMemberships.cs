using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OutHouse.Server.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMemberships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_AspNetUsers_UserId",
                table: "Memberships");

            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Outhouses_OuthouseId",
                table: "Memberships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Memberships");

            migrationBuilder.RenameTable(
                name: "Memberships",
                newName: "Members");

            migrationBuilder.RenameIndex(
                name: "IX_Memberships_OuthouseId",
                table: "Members",
                newName: "IX_Members_OuthouseId");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Members",
                table: "Members",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Outhouses_OuthouseId",
                table: "Members",
                column: "OuthouseId",
                principalTable: "Outhouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Outhouses_OuthouseId",
                table: "Members");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Members",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Members");

            migrationBuilder.RenameTable(
                name: "Members",
                newName: "Memberships");

            migrationBuilder.RenameIndex(
                name: "IX_Members_OuthouseId",
                table: "Memberships",
                newName: "IX_Memberships_OuthouseId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Memberships",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Memberships",
                table: "Memberships",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserId",
                table: "Memberships",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_AspNetUsers_UserId",
                table: "Memberships",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Outhouses_OuthouseId",
                table: "Memberships",
                column: "OuthouseId",
                principalTable: "Outhouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
