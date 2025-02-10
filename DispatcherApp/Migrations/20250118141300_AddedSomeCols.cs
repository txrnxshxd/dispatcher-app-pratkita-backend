using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dispatcher",
                table: "Planes",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastCheckDate",
                schema: "dispatcher",
                table: "Planes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "ManufactureYear",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                maxLength: 4,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PassengerCapacity",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                maxLength: 3,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "Planes",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlaneTypeId",
                schema: "dispatcher",
                table: "Planes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                schema: "dispatcher",
                table: "Planes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TailNumber",
                schema: "dispatcher",
                table: "Planes",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");


            migrationBuilder.CreateTable(
                name: "PlaneTypes",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneTypes", x => x.Id);
                });

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
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    PassportSeries = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    PassportNumber = table.Column<int>(type: "integer", maxLength: 6, nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Airports",
                schema: "dispatcher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    ICAO = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    IATA = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    CityId = table.Column<int>(type: "integer", nullable: false)
                });

            migrationBuilder.CreateIndex(
                name: "IX_Planes_PlaneTypeId",
                schema: "dispatcher",
                table: "Planes",
                column: "PlaneTypeId");


            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                schema: "dispatcher",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Planes_PlaneTypes_PlaneTypeId",
                schema: "dispatcher",
                table: "Planes",
                column: "PlaneTypeId",
                principalSchema: "dispatcher",
                principalTable: "PlaneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Planes_PlaneTypes_PlaneTypeId",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropTable(
                name: "Airports",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "PlaneTypes",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "dispatcher");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "dispatcher");

            migrationBuilder.DropIndex(
                name: "IX_Planes_PlaneTypeId",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "LastCheckDate",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "ManufactureYear",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "PassengerCapacity",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "PlaneTypeId",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.DropColumn(
                name: "TailNumber",
                schema: "dispatcher",
                table: "Planes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "dispatcher",
                table: "Planes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);
        }
    }
}
