using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VoroFit.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStructureExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WorkoutHistories");

            migrationBuilder.RenameColumn(
                name: "LastUpdated",
                table: "WorkoutHistories",
                newName: "ExecutionDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "WorkoutHistories",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "WorkoutHistories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "WorkoutHistories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanDayId",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanId",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WorkoutPlanWeekId",
                table: "WorkoutHistories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StudentUserExtensionId",
                table: "Exercises",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WorkoutHistoryExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    PlannedSets = table.Column<int>(type: "integer", nullable: false),
                    PlannedReps = table.Column<int>(type: "integer", nullable: false),
                    ExecutedSets = table.Column<int>(type: "integer", nullable: false),
                    ExecutedReps = table.Column<int>(type: "integer", nullable: false),
                    PlannedWeight = table.Column<float>(type: "real", nullable: true),
                    ExecutedWeight = table.Column<float>(type: "real", nullable: true),
                    RestInSeconds = table.Column<int>(type: "integer", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutHistoryExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutHistoryExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutHistoryExercises_WorkoutHistories_WorkoutHistoryId",
                        column: x => x.WorkoutHistoryId,
                        principalTable: "WorkoutHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlans_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserExtensionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlanWeeks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekNumber = table.Column<int>(type: "integer", nullable: false),
                    WorkoutPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlanWeeks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanWeeks_WorkoutPlans_WorkoutPlanId",
                        column: x => x.WorkoutPlanId,
                        principalTable: "WorkoutPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlanDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    WorkoutPlanWeekId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlanDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanDays_WorkoutPlanWeeks_WorkoutPlanWeekId",
                        column: x => x.WorkoutPlanWeekId,
                        principalTable: "WorkoutPlanWeeks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutPlanExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutPlanDayId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Sets = table.Column<int>(type: "integer", nullable: false),
                    Reps = table.Column<int>(type: "integer", nullable: false),
                    RestInSeconds = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Alternative = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPlanExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutPlanExercises_WorkoutPlanDays_WorkoutPlanDayId",
                        column: x => x.WorkoutPlanDayId,
                        principalTable: "WorkoutPlanDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanDayId",
                table: "WorkoutHistories",
                column: "WorkoutPlanDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanId",
                table: "WorkoutHistories",
                column: "WorkoutPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistories_WorkoutPlanWeekId",
                table: "WorkoutHistories",
                column: "WorkoutPlanWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_StudentUserExtensionId",
                table: "Exercises",
                column: "StudentUserExtensionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistoryExercises_ExerciseId",
                table: "WorkoutHistoryExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutHistoryExercises_WorkoutHistoryId",
                table: "WorkoutHistoryExercises",
                column: "WorkoutHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanDays_WorkoutPlanWeekId",
                table: "WorkoutPlanDays",
                column: "WorkoutPlanWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanExercises_ExerciseId",
                table: "WorkoutPlanExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanExercises_WorkoutPlanDayId",
                table: "WorkoutPlanExercises",
                column: "WorkoutPlanDayId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlans_StudentId",
                table: "WorkoutPlans",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPlanWeeks_WorkoutPlanId",
                table: "WorkoutPlanWeeks",
                column: "WorkoutPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Students_StudentUserExtensionId",
                table: "Exercises",
                column: "StudentUserExtensionId",
                principalTable: "Students",
                principalColumn: "UserExtensionId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanDays_WorkoutPlanDayId",
                table: "WorkoutHistories",
                column: "WorkoutPlanDayId",
                principalTable: "WorkoutPlanDays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanWeeks_WorkoutPlanWeekId",
                table: "WorkoutHistories",
                column: "WorkoutPlanWeekId",
                principalTable: "WorkoutPlanWeeks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutHistories",
                column: "WorkoutPlanId",
                principalTable: "WorkoutPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Students_StudentUserExtensionId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanDays_WorkoutPlanDayId",
                table: "WorkoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlanWeeks_WorkoutPlanWeekId",
                table: "WorkoutHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutHistories_WorkoutPlans_WorkoutPlanId",
                table: "WorkoutHistories");

            migrationBuilder.DropTable(
                name: "WorkoutHistoryExercises");

            migrationBuilder.DropTable(
                name: "WorkoutPlanExercises");

            migrationBuilder.DropTable(
                name: "WorkoutPlanDays");

            migrationBuilder.DropTable(
                name: "WorkoutPlanWeeks");

            migrationBuilder.DropTable(
                name: "WorkoutPlans");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanDayId",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanId",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutHistories_WorkoutPlanWeekId",
                table: "WorkoutHistories");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_StudentUserExtensionId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanDayId",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanId",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "WorkoutPlanWeekId",
                table: "WorkoutHistories");

            migrationBuilder.DropColumn(
                name: "StudentUserExtensionId",
                table: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExecutionDate",
                table: "WorkoutHistories",
                newName: "LastUpdated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "WorkoutHistories",
                newName: "CreatedDate");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WorkoutHistories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutHistoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Alternative = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Reps = table.Column<int>(type: "integer", nullable: false),
                    RestInSeconds = table.Column<int>(type: "integer", nullable: false),
                    Sets = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "UserExtensionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutExercises_WorkoutHistories_WorkoutHistoryId",
                        column: x => x.WorkoutHistoryId,
                        principalTable: "WorkoutHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_StudentId",
                table: "WorkoutExercises",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutHistoryId",
                table: "WorkoutExercises",
                column: "WorkoutHistoryId");
        }
    }
}
