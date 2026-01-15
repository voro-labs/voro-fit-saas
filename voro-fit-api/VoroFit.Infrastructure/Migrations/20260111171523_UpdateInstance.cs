using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionUserId",
                table: "Instance");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceExtensions_UserExtensions_UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.DropIndex(
                name: "IX_InstanceExtensions_UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.DropIndex(
                name: "IX_Instance_UserExtensionUserId",
                table: "Instance");

            migrationBuilder.DropColumn(
                name: "UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.DropColumn(
                name: "UserExtensionUserId",
                table: "Instance");

            migrationBuilder.AddColumn<Guid>(
                name: "UserExtensionId",
                table: "Instance",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Instance_UserExtensionId",
                table: "Instance",
                column: "UserExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionId",
                table: "Instance",
                column: "UserExtensionId",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionId",
                table: "Instance");

            migrationBuilder.DropIndex(
                name: "IX_Instance_UserExtensionId",
                table: "Instance");

            migrationBuilder.DropColumn(
                name: "UserExtensionId",
                table: "Instance");

            migrationBuilder.AddColumn<Guid>(
                name: "UserExtensionId",
                table: "InstanceExtensions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserExtensionUserId",
                table: "Instance",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InstanceExtensions_UserExtensionId",
                table: "InstanceExtensions",
                column: "UserExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_Instance_UserExtensionUserId",
                table: "Instance",
                column: "UserExtensionUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionUserId",
                table: "Instance",
                column: "UserExtensionUserId",
                principalTable: "UserExtensions",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceExtensions_UserExtensions_UserExtensionId",
                table: "InstanceExtensions",
                column: "UserExtensionId",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
