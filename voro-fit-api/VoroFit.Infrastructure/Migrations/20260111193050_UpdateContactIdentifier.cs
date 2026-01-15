using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactIdentifier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Contacts_ContactId",
                table: "Chats");

            migrationBuilder.AddColumn<Guid>(
                name: "ContactId",
                table: "UserExtensions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserExtensionId",
                table: "Contacts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Contacts_ContactId",
                table: "Chats",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_UserExtensions_Id",
                table: "Contacts",
                column: "Id",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Contacts_ContactId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_UserExtensions_Id",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "UserExtensions");

            migrationBuilder.DropColumn(
                name: "UserExtensionId",
                table: "Contacts");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Contacts_ContactId",
                table: "Chats",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id");
        }
    }
}
