using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddBrandAndEquipmentCrud : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Volvo" },
                    { 2, "Ford" },
                    { 3, "Tesla" },
                    { 4, "Toyota" },
                    { 5, "BMW" }
                });

            migrationBuilder.InsertData(
                table: "VehicleEquipments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Parking Sensor" },
                    { 2, "Rear view window" },
                    { 3, "Leather Seats" },
                    { 4, "Sunroof" },
                    { 5, "Bluetooth" }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "BrandId", "Model", "Year" },
                values: new object[,]
                {
                    { 1, 1, "V70", 2020 },
                    { 2, 2, "Focus", 2022 },
                    { 3, 3, "Model S", 2021 },
                    { 4, 4, "Camry", 2019 },
                    { 5, 5, "X5", 2021 }
                });

            migrationBuilder.InsertData(
                table: "VehicleVehicleEquipment",
                columns: new[] { "EquipmentId", "VehiclesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 1 },
                    { 3, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "VehicleVehicleEquipment",
                keyColumns: new[] { "EquipmentId", "VehiclesId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VehicleEquipments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Vehicles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
