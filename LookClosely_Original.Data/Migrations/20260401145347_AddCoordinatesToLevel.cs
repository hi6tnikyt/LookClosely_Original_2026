using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookClosely_Original.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoordinatesToLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TargetRadius",
                table: "Levels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetX",
                table: "Levels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TargetY",
                table: "Levels",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TargetRadius", "TargetX", "TargetY" },
                values: new object[] { 5.0, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "TargetRadius", "TargetX", "TargetY" },
                values: new object[] { 5.0, 0.0, 0.0 });

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "TargetRadius", "TargetX", "TargetY" },
                values: new object[] { 5.0, 0.0, 0.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetRadius",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "TargetX",
                table: "Levels");

            migrationBuilder.DropColumn(
                name: "TargetY",
                table: "Levels");
        }
    }
}
