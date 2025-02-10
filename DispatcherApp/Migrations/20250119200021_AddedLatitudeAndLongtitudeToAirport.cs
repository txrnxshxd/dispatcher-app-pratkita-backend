using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedLatitudeAndLongtitudeToAirport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Latitude",
                schema: "dispatcher",
                table: "Airports",
                type: "numeric(9,6)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Longitude",
                schema: "dispatcher",
                table: "Airports",
                type: "numeric(9,6)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "dispatcher",
                table: "Airports");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "dispatcher",
                table: "Airports");
        }
    }
}
