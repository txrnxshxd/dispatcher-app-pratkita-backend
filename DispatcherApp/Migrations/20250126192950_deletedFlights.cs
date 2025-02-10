using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class deletedFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights",
                schema: "dispatcher");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CaptainId = table.Column<int>(type: "integer", nullable: false),
                    CarrierCompanyId = table.Column<int>(type: "integer", nullable: false),
                    From = table.Column<int>(type: "integer", nullable: false),
                    PilotId = table.Column<int>(type: "integer", nullable: false),
                    PlaneId = table.Column<int>(type: "integer", nullable: false),
                    To = table.Column<int>(type: "integer", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Fullness = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_From",
                        column: x => x.From,
                        principalSchema: "dispatcher",
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_To",
                        column: x => x.To,
                        principalSchema: "dispatcher",
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_CarrierCompanies_CarrierCompanyId",
                        column: x => x.CarrierCompanyId,
                        principalSchema: "dispatcher",
                        principalTable: "CarrierCompanies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Pilots_CaptainId",
                        column: x => x.CaptainId,
                        principalSchema: "dispatcher",
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalSchema: "dispatcher",
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Flights_Planes_PlaneId",
                        column: x => x.PlaneId,
                        principalSchema: "dispatcher",
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CaptainId",
                schema: "dispatcher",
                table: "Flights",
                column: "CaptainId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_CarrierCompanyId",
                schema: "dispatcher",
                table: "Flights",
                column: "CarrierCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_From",
                schema: "dispatcher",
                table: "Flights",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PilotId",
                schema: "dispatcher",
                table: "Flights",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PlaneId",
                schema: "dispatcher",
                table: "Flights",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_To",
                schema: "dispatcher",
                table: "Flights",
                column: "To");
        }
    }
}
