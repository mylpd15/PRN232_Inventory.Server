using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareSync.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
