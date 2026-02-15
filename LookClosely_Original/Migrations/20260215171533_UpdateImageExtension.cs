using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookClosely_Original.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageExtension : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/images/levels/level1.webp");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImagePath",
                value: "/images/levels/level1.jpg");
        }
    }
}
