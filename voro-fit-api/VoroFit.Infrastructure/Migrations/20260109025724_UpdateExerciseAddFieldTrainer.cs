using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExerciseAddFieldTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Exercises",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainerId",
                table: "Exercises",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_UserExtensions_TrainerId",
                table: "Exercises",
                column: "TrainerId",
                principalTable: "UserExtensions",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_UserExtensions_TrainerId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_TrainerId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Exercises");
        }
    }
}
