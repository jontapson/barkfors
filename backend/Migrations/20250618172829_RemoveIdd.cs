using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleEquipments_Vehicles_VehicleId",
                table: "VehicleEquipments");

            migrationBuilder.DropIndex(
                name: "IX_VehicleEquipments_VehicleId",
                table: "VehicleEquipments");

            migrationBuilder.DropColumn(
                name: "VehicleId",
                table: "VehicleEquipments");

            migrationBuilder.CreateTable(
                name: "VehicleVehicleEquipment",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "int", nullable: false),
                    VehiclesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleVehicleEquipment", x => new { x.EquipmentId, x.VehiclesId });
                    table.ForeignKey(
                        name: "FK_VehicleVehicleEquipment_VehicleEquipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "VehicleEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleVehicleEquipment_Vehicles_VehiclesId",
                        column: x => x.VehiclesId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VehicleVehicleEquipment_VehiclesId",
                table: "VehicleVehicleEquipment",
                column: "VehiclesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleVehicleEquipment");

            migrationBuilder.AddColumn<int>(
                name: "VehicleId",
                table: "VehicleEquipments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 1,
                column: "VehicleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 2,
                column: "VehicleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 3,
                column: "VehicleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 4,
                column: "VehicleId",
                value: null);

            migrationBuilder.UpdateData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 5,
                column: "VehicleId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleEquipments_VehicleId",
                table: "VehicleEquipments",
                column: "VehicleId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleEquipments_Vehicles_VehicleId",
                table: "VehicleEquipments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
