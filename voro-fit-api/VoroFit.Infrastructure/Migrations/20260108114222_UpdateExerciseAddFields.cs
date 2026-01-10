using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExerciseAddFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Media",
                table: "Exercises",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "Exercises",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Media",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "Exercises");
        }
    }
}
