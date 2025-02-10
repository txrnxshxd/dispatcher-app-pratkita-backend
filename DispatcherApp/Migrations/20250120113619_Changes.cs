using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Altitude",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Course",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Speed",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "VerticalSpeed",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "MaxSpeed",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxSpeed",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.AddColumn<int>(
                name: "Altitude",
                schema: "dispatcher",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Course",
                schema: "dispatcher",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "dispatcher",
                table: "Flights",
                type: "numeric(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "dispatcher",
                table: "Flights",
                type: "numeric(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Speed",
                schema: "dispatcher",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VerticalSpeed",
                schema: "dispatcher",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
