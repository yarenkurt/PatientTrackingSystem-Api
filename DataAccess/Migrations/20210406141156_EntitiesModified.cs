using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EntitiesModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAdvices_Doctors_DoctorId",
                table: "DoctorAdvices");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAdvices_Patients_PatientId",
                table: "DoctorAdvices");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Diseases_DiseaseId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_PersonLoginHistories_IpAddress",
                table: "PersonLoginHistories");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DiseaseId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiseases_DiseaseId",
                table: "PatientDiseases");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiseases_PatientId",
                table: "PatientDiseases");

            migrationBuilder.DropIndex(
                name: "IX_DoctorAdvices_DoctorId",
                table: "DoctorAdvices");

            migrationBuilder.DropIndex(
                name: "IX_DoctorAdvices_PatientId",
                table: "DoctorAdvices");

            migrationBuilder.DropColumn(
                name: "DiseaseId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "DoctorAdvices");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)");

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiredDate",
                table: "Persons",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "Convert(Date,GetDate())");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "PersonLoginHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "DoctorAdvices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserName",
                table: "DoctorAdvices",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValueSql: "space(0)");

            migrationBuilder.CreateTable(
                name: "DoctorPatients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorPatients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorPatients_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_DiseaseId",
                table: "PatientDiseases",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_PatientId_DiseaseId",
                table: "PatientDiseases",
                columns: new[] { "PatientId", "DiseaseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAdvices_DepartmentId",
                table: "DoctorAdvices",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatients_DoctorId",
                table: "DoctorPatients",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorPatients_PatientId_DoctorId",
                table: "DoctorPatients",
                columns: new[] { "PatientId", "DoctorId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAdvices_Deparments_DepartmentId",
                table: "DoctorAdvices",
                column: "DepartmentId",
                principalTable: "Deparments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorAdvices_Deparments_DepartmentId",
                table: "DoctorAdvices");

            migrationBuilder.DropTable(
                name: "DoctorPatients");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiseases_DiseaseId",
                table: "PatientDiseases");

            migrationBuilder.DropIndex(
                name: "IX_PatientDiseases_PatientId_DiseaseId",
                table: "PatientDiseases");

            migrationBuilder.DropIndex(
                name: "IX_DoctorAdvices_DepartmentId",
                table: "DoctorAdvices");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiredDate",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "CreatedUserName",
                table: "DoctorAdvices");

            migrationBuilder.AlterColumn<string>(
                name: "IpAddress",
                table: "PersonLoginHistories",
                type: "nvarchar(450)",
                nullable: false,
                defaultValueSql: "space(0)",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValueSql: "space(0)");

            migrationBuilder.AddColumn<int>(
                name: "DiseaseId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "DoctorAdvices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "DoctorAdvices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PersonLoginHistories_IpAddress",
                table: "PersonLoginHistories",
                column: "IpAddress",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DiseaseId",
                table: "Patients",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_DiseaseId",
                table: "PatientDiseases",
                column: "DiseaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiseases_PatientId",
                table: "PatientDiseases",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAdvices_DoctorId",
                table: "DoctorAdvices",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorAdvices_PatientId",
                table: "DoctorAdvices",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAdvices_Doctors_DoctorId",
                table: "DoctorAdvices",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorAdvices_Patients_PatientId",
                table: "DoctorAdvices",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Diseases_DiseaseId",
                table: "Patients",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
