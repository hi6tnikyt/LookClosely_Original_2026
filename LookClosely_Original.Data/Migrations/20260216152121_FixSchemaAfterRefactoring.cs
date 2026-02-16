using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookClosely_Original.Migrations
{
    /// <inheritdoc />
    public partial class FixSchemaAfterRefactoring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Levels_LevelId",
                table: "Scores");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Scores",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LevelId1",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ApplicationUserId",
                table: "Scores",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_LevelId1",
                table: "Scores",
                column: "LevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_AspNetUsers_ApplicationUserId",
                table: "Scores",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Levels_LevelId",
                table: "Scores",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Levels_LevelId1",
                table: "Scores",
                column: "LevelId1",
                principalTable: "Levels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Scores_AspNetUsers_ApplicationUserId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Levels_LevelId",
                table: "Scores");

            migrationBuilder.DropForeignKey(
                name: "FK_Scores_Levels_LevelId1",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_ApplicationUserId",
                table: "Scores");

            migrationBuilder.DropIndex(
                name: "IX_Scores_LevelId1",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Scores");

            migrationBuilder.DropColumn(
                name: "LevelId1",
                table: "Scores");

            migrationBuilder.AddForeignKey(
                name: "FK_Scores_Levels_LevelId",
                table: "Scores",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
