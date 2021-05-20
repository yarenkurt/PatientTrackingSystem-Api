using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DeleteBehaviors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerPools_QuestionPools_QuestionPoolId",
                table: "AnswerPools");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerPools_QuestionPools_QuestionPoolId",
                table: "AnswerPools",
                column: "QuestionPoolId",
                principalTable: "QuestionPools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnswerPools_QuestionPools_QuestionPoolId",
                table: "AnswerPools");

            migrationBuilder.AddForeignKey(
                name: "FK_AnswerPools_QuestionPools_QuestionPoolId",
                table: "AnswerPools",
                column: "QuestionPoolId",
                principalTable: "QuestionPools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
