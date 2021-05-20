using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EntitiesModified3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Day",
                table: "PatientQuestions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "DoctorAdvices");

            migrationBuilder.DropColumn(
                name: "ReadingTime",
                table: "DoctorAdvices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Day",
                table: "PatientQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "DoctorAdvices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadingTime",
                table: "DoctorAdvices",
                type: "datetime2",
                nullable: true);
        }
    }
}
