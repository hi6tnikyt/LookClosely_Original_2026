using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LookClosely_Original.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTargetObjectNameToLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AddColumn<string>(
                name: "TargetObjectName",
                table: "Levels",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetObjectName",
                table: "Levels");

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Difficulty", "ImagePath", "IsDeleted", "Name", "TargetRadius", "TargetX", "TargetY" },
                values: new object[,]
                {
                    { 1, "Easy", "/images/levels/level1.webp", false, "Стаята на детектива", 5.0, 0.0, 0.0 },
                    { 2, "Medium", "/images/levels/level2.jpg", false, "Изоставената библиотека", 5.0, 0.0, 0.0 },
                    { 3, "Hard", "/images/levels/level3.jpg", false, "Тайното мазе", 5.0, 0.0, 0.0 }
                });
        }
    }
}
