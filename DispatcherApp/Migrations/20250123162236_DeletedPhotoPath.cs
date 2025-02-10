using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class DeletedPhotoPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "CarrierCompanies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "Planes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "Countries",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "CarrierCompanies",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
