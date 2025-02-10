using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class changedAirportLatitudeAndLongitudeToDouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Longitude",
                schema: "dispatcher",
                table: "Airports",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,6)");

            migrationBuilder.AlterColumn<double>(
                name: "Latitude",
                schema: "dispatcher",
                table: "Airports",
                type: "double precision",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(9,6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longitude",
                schema: "dispatcher",
                table: "Airports",
                type: "numeric(9,6)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                schema: "dispatcher",
                table: "Airports",
                type: "numeric(9,6)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double precision");
        }
    }
}
