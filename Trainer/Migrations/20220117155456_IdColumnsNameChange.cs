using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainer.Migrations
{
    public partial class IdColumnsNameChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TrainingExerciseID",
                table: "TrainingExercise",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "TrainingID",
                table: "Training",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "ExerciseID",
                table: "Exercise",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "TrainingExercise",
                newName: "TrainingExerciseID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Training",
                newName: "TrainingID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Exercise",
                newName: "ExerciseID");
        }
    }
}
