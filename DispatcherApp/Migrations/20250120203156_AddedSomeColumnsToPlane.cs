using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeColumnsToPlane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.AddColumn<int>(
                name: "CruisingSpeed",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LandingSpeed",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxAltitude",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TakeoffSpeed",
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
                name: "CruisingSpeed",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "LandingSpeed",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "MaxAltitude",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "TakeoffSpeed",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "dispatcher",
                table: "Planes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
