using Microsoft.EntityFrameworkCore.Migrations;

namespace Trainer.Migrations
{
    public partial class DataChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientID",
                table: "TrainingExercise",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercise_ClientID",
                table: "TrainingExercise",
                column: "ClientID");

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingExercise_Client_ClientID",
                table: "TrainingExercise",
                column: "ClientID",
                principalTable: "Client",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrainingExercise_Client_ClientID",
                table: "TrainingExercise");

            migrationBuilder.DropIndex(
                name: "IX_TrainingExercise_ClientID",
                table: "TrainingExercise");

            migrationBuilder.DropColumn(
                name: "ClientID",
                table: "TrainingExercise");
        }
    }
}
