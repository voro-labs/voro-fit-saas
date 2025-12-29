using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedWorkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutPlanWeeks",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutPlanWeeks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "WorkoutPlanWeeks",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutPlans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutPlans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutPlanExercises",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutPlanExercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutPlanDays",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutPlanDays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "WorkoutPlanDays",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutHistoryExercises",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutHistoryExercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "WorkoutHistories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WorkoutHistories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Measurements",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Measurements",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "MealPlans",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MealPlans",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "MealPlanMeals",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MealPlanMeals",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "MealPlanDays",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "MealPlanDays",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "Exercises",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Exercises",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutPlanWeeks");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutPlanWeeks");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WorkoutPlanWeeks");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutPlans");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutPlanExercises");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutPlanExercises");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutPlanDays");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutPlanDays");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WorkoutPlanDays");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutHistoryExercises");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutHistoryExercises");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Measurements");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MealPlans");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MealPlanMeals");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MealPlanMeals");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "MealPlanDays");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "MealPlanDays");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Exercises");
        }
    }
}
