using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WareSync.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Warehouses_WarehouseID",
                table: "Transfers");

            // 1. Add as nullable
            migrationBuilder.AddColumn<int>(
                name: "WarehouseID",
                table: "Orders",
                type: "int",
                nullable: true);

            // 2. Set a valid WarehouseID for all existing orders (replace 1 with a real WarehouseID if needed)
            migrationBuilder.Sql("UPDATE Orders SET WarehouseID = 1 WHERE WarehouseID IS NULL");

            // 3. Alter to non-nullable
            migrationBuilder.AlterColumn<int>(
                name: "WarehouseID",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_WarehouseID",
                table: "Orders",
                column: "WarehouseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Warehouses_WarehouseID",
                table: "Transfers",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Warehouses_WarehouseID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Warehouses_WarehouseID",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Orders_WarehouseID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "WarehouseID",
                table: "Orders");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Warehouses_WarehouseID",
                table: "Transfers",
                column: "WarehouseID",
                principalTable: "Warehouses",
                principalColumn: "WarehouseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
