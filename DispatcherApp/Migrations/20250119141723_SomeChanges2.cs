using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DispatcherApp.Migrations
{
    /// <inheritdoc />
    public partial class SomeChanges2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityId",
                schema: "dispatcher",
                table: "Pilots",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pilots_CityId",
                schema: "dispatcher",
                table: "Pilots",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pilots_Cities_CityId",
                schema: "dispatcher",
                table: "Pilots",
                column: "CityId",
                principalSchema: "dispatcher",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pilots_Cities_CityId",
                schema: "dispatcher",
                table: "Pilots");

            migrationBuilder.DropIndex(
                name: "IX_Pilots_CityId",
                schema: "dispatcher",
                table: "Pilots");

            migrationBuilder.DropColumn(
                name: "CityId",
                schema: "dispatcher",
                table: "Pilots");
        }
    }
}
