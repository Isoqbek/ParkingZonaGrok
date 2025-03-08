using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeVehicleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_AssignedSpotId",
                table: "Vehicles",
                column: "AssignedSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_ParkingSpots_AssignedSpotId",
                table: "Vehicles",
                column: "AssignedSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_ParkingSpots_AssignedSpotId",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_AssignedSpotId",
                table: "Vehicles");
        }
    }
}
