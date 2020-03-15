using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class addprecision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Order Details",
                table: "Order Details");

            migrationBuilder.RenameTable(
                name: "Order Details",
                newName: "OrderDetails");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallTotal",
                table: "Orders",
                type: "decimal(18,4)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "OrderDetails",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "Order Details");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "OverallTotal",
                table: "Orders",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Order Details",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order Details",
                table: "Order Details",
                column: "Id");
        }
    }
}
