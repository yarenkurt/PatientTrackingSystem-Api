using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class IsActivePropertyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Patients",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Doctors",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsBlocked",
                table: "Admins",
                newName: "IsActive");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "QuestionPools",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PatientQuestions",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PatientDiseases",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PatientAnswers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Diseases",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Deparments",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AnswerPools",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "QuestionPools");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PatientQuestions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PatientDiseases");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PatientAnswers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Diseases");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Deparments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AnswerPools");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Patients",
                newName: "IsBlocked");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Doctors",
                newName: "IsBlocked");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Admins",
                newName: "IsBlocked");
        }
    }
}
