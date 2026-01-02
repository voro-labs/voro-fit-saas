using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanDayId1",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanId1",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanWeekId1",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanDayId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanDayId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanWeekId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanWeekId1");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanDays_WorkoutPlanDayId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanDayId1",
                principalTable: "WorkoutPlanDays",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanWeeks_WorkoutPlanWeekId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanWeekId1",
                principalTable: "WorkoutPlanWeeks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlans_WorkoutPlanId1",
                table: "WorkoutHistories",
                column: "WorkoutPlanId1",
                principalTable: "WorkoutPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanDays_WorkoutPlanDayId1",
                table: "WorkoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanWeeks_WorkoutPlanWeekId1",
                table: "WorkoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlans_WorkoutPlanId1",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanDayId1",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanId1",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanWeekId1",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanDayId1",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanId1",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanWeekId1",
                table: "WorkoutHistories");
        }
    }
}
