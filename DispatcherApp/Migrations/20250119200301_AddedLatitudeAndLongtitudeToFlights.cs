using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedLatitudeAndLongtitudeToFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "dispatcher",
                table: "Flights");
        }
    }
}
