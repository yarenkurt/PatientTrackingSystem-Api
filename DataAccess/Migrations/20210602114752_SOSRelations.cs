using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SOSRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
             migrationBuilder.DropColumn(
                name: "Time",
                table: "Appointments");
             */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Appointments",
                type: "time",
                nullable: false,
                defaultValueSql: "'00:00'");
        }
    }
}
