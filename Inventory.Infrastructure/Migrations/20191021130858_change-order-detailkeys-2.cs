using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class changeorderdetailkeys2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductSizes_ProductSizesId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ProductSizesId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ProductSizesId",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "Order Details");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Order Details",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Order Details",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "Order Details",
                newName: "ProductsOrder_Details");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "Order Details",
                newName: "OrdersOrder_Details");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "Order Details",
                type: "money",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<short>(
                name: "Quantity",
                table: "Order Details",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(short));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order Details",
                table: "Order Details",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Order Details_ProductSizeId",
                table: "Order Details",
                column: "ProductSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Orders",
                table: "Order Details",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Products",
                table: "Order Details",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Details_Products_sizes",
                table: "Order Details",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Orders",
                table: "Order Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Products",
                table: "Order Details");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Details_Products_sizes",
                table: "Order Details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order Details",
                table: "Order Details");

            migrationBuilder.DropIndex(
                name: "IX_Order Details_ProductSizeId",
                table: "Order Details");

            migrationBuilder.RenameTable(
                name: "Order Details",
                newName: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "OrderDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "ProductsOrder_Details",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "OrdersOrder_Details",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.AlterColumn<decimal>(
                name: "UnitPrice",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "money");

            migrationBuilder.AlterColumn<short>(
                name: "Quantity",
                table: "OrderDetails",
                nullable: false,
                oldClrType: typeof(short),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductSizesId",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductSizesId",
                table: "OrderDetails",
                column: "ProductSizesId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductSizes_ProductSizesId",
                table: "OrderDetails",
                column: "ProductSizesId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
