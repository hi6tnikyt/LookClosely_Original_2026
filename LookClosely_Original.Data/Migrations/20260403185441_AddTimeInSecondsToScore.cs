using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookClosely_Original.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeInSecondsToScore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimeInSeconds",
                table: "Scores",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TargetX", "TargetY" },
                values: new object[] { 18.98, 56.060000000000002 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeInSeconds",
                table: "Scores");

            migrationBuilder.UpdateData(
                table: "Levels",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "TargetX", "TargetY" },
                values: new object[] { 45.0, 55.0 });
        }
    }
}
