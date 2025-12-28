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
                name: "FK_Chats_Instances_InstanceId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Instances_UserExtensions_UserExtensionUserId",
                table: "Instances");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instances",
                table: "Instances");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Instances");

            migrationBuilder.RenameTable(
                name: "Instances",
                newName: "Instance");

            migrationBuilder.RenameIndex(
                name: "IX_Instances_UserExtensionUserId",
                table: "Instance",
                newName: "IX_Instance_UserExtensionUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "InstanceExtensionInstanceId",
                table: "Chats",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instance",
                table: "Instance",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "InstanceExtensions",
                columns: table => new
                {
                    InstanceId = table.Column<Guid>(type: "uuid", nullable: false),
                    Hash = table.Column<string>(type: "text", nullable: false),
                    Base64 = table.Column<string>(type: "text", nullable: false),
                    Integration = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ConnectedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceExtensions", x => x.InstanceId);
                    table.ForeignKey(
                        name: "FK_InstanceExtensions_Instance_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionUserId",
                table: "Instance",
                column: "UserExtensionUserId",
                principalTable: "UserExtensions",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_InstanceExtensions_InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Instance_InstanceId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Instance_UserExtensions_UserExtensionUserId",
                table: "Instance");

            migrationBuilder.DropTable(
                name: "InstanceExtensions");

            migrationBuilder.DropIndex(
                name: "IX_Chats_InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instance",
                table: "Instance");

            migrationBuilder.DropColumn(
                name: "InstanceExtensionInstanceId",
                table: "Chats");

            migrationBuilder.RenameTable(
                name: "Instance",
                newName: "Instances");

            migrationBuilder.RenameIndex(
                name: "IX_Instance_UserExtensionUserId",
                table: "Instances",
                newName: "IX_Instances_UserExtensionUserId");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "Instances",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instances",
                table: "Instances",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Instances_InstanceId",
                table: "Chats",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Instances_UserExtensions_UserExtensionUserId",
                table: "Instances",
                column: "UserExtensionUserId",
                principalTable: "UserExtensions",
                principalColumn: "UserId");
        }
    }
}
