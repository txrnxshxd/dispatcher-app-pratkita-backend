using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarrierCompanies",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    PhotoPath = table.Column<string>(type: "text", nullable: false),
                    CountryPhotoPath = table.Column<string>(type: "text", nullable: false)
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights",
                column: "CarrierCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrierCompanies_CountryId",
                schema: "dispatcher",
                table: "CarrierCompanies",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_CarrierCompanies_CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights",
                column: "CarrierCompanyId",
                principalSchema: "dispatcher",
                principalTable: "CarrierCompanies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_CarrierCompanies_CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "CarrierCompanies",
                schema: "dispatcher");

            migrationBuilder.DropIndex(
                name: "IX_Flights_CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights");
        }
    }
}
