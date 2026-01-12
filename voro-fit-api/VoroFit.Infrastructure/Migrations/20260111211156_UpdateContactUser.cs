using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContactUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_UserExtensions_Id",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserExtensionId",
                table: "Contacts",
                column: "UserExtensionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_UserExtensions_UserExtensionId",
                table: "Contacts",
                column: "UserExtensionId",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_UserExtensions_UserExtensionId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_UserExtensionId",
                table: "Contacts");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_UserExtensions_Id",
                table: "Contacts",
                column: "Id",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
