using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LookClosely_Original.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "Id", "Difficulty", "ImagePath", "IsDeleted", "Name", "TargetObjectName", "TargetRadius", "TargetX", "TargetY" },
                values: new object[,]
                {
                    { 1, "Easy", "/images/levels/level1.webp", false, "Стаята на детектива", "Лупа", 5.0, 45.0, 55.0 },
                    { 2, "Medium", "/images/levels/level2.jpg", false, "Изоставената библиотека", "Стара книга", 5.0, 30.0, 40.0 },
                    { 3, "Hard", "/images/levels/level3.jpg", false, "Тайното мазе", "Златен ключ", 5.0, 70.0, 20.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
