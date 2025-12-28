using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInstanceToFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_InstanceExtensions_InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Instance_InstanceId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "InstanceId",
                table: "Chats",
                newName: "InstanceExtensionId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_InstanceId",
                table: "Chats",
                newName: "IX_Chats_InstanceExtensionId");

            migrationBuilder.AddColumn<Guid>(
                name: "UserExtensionId",
                table: "InstanceExtensions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_InstanceExtensions_UserExtensionId",
                table: "InstanceExtensions",
                column: "UserExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_InstanceExtensions_InstanceExtensionId",
                table: "Chats",
                column: "InstanceExtensionId",
                principalTable: "InstanceExtensions",
                principalColumn: "InstanceId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstanceExtensions_UserExtensions_UserExtensionId",
                table: "InstanceExtensions",
                column: "UserExtensionId",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_InstanceExtensions_InstanceExtensionId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_InstanceExtensions_UserExtensions_UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.DropIndex(
                name: "IX_InstanceExtensions_UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.DropColumn(
                name: "UserExtensionId",
                table: "InstanceExtensions");

            migrationBuilder.RenameColumn(
                name: "InstanceExtensionId",
                table: "Chats",
                newName: "InstanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_InstanceExtensionId",
                table: "Chats",
                newName: "IX_Chats_InstanceId");

            migrationBuilder.AddColumn<Guid>(
                name: "InstanceExtensionInstanceId",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_InstanceExtensionInstanceId",
                table: "Chats",
                column: "InstanceExtensionInstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_InstanceExtensions_InstanceExtensionInstanceId",
                table: "Chats",
                column: "InstanceExtensionInstanceId",
                principalTable: "InstanceExtensions",
                principalColumn: "InstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Instance_InstanceId",
                table: "Chats",
                column: "InstanceId",
                principalTable: "Instance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
