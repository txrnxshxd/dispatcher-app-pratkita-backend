using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Employees_CaptainId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Employees_PilotId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "dispatcher");

            migrationBuilder.CreateTable(
                name: "Pilots",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilots_CaptainId",
                schema: "dispatcher",
                table: "Flights",
                column: "CaptainId",
                principalSchema: "dispatcher",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                schema: "dispatcher",
                table: "Flights",
                column: "PilotId",
                principalSchema: "dispatcher",
                principalTable: "Pilots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilots_CaptainId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Pilots_PilotId",
                schema: "dispatcher",
                table: "Flights");

            migrationBuilder.DropTable(
                name: "Pilots",
                schema: "dispatcher");

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PassportNumber = table.Column<int>(type: "integer", maxLength: 6, nullable: false),
                    PassportSeries = table.Column<int>(type: "integer", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Positions_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "dispatcher",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                schema: "dispatcher",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Employees_CaptainId",
                schema: "dispatcher",
                table: "Flights",
                column: "CaptainId",
                principalSchema: "dispatcher",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Employees_PilotId",
                schema: "dispatcher",
                table: "Flights",
                column: "PilotId",
                principalSchema: "dispatcher",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
