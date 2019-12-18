using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class removeorderproductrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_Details_Products",
            //    table: "Order Details");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order_Details_Products_sizes",
            //    table: "Order Details");

            //migrationBuilder.DropIndex(
            //    name: "IX_Order Details_ProductSizeId",
            //    table: "Order Details");

            //migrationBuilder.AlterColumn<int>(
            //    name: "ProductSizeId",
            //    table: "Order Details",
            //    nullable: false,
            //    oldClrType: typeof(Guid));

            //migrationBuilder.AlterColumn<int>(
            //    name: "ProductID",
            //    table: "Order Details",
            //    nullable: false,
            //    oldClrType: typeof(Guid));

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ProductSizesId",
            //    table: "Order Details",
            //    nullable: true);

            //migrationBuilder.AddColumn<Guid>(
            //    name: "ProductsId",
            //    table: "Order Details",
            //    nullable: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Order Details_ProductSizesId",
            //    table: "Order Details",
            //    column: "ProductSizesId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Order Details_ProductsId",
            //    table: "Order Details",
            //    column: "ProductsId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order Details_ProductSizes_ProductSizesId",
            //    table: "Order Details",
            //    column: "ProductSizesId",
            //    principalTable: "ProductSizes",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order Details_Products_ProductsId",
            //    table: "Order Details",
            //    column: "ProductsId",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order Details_ProductSizes_ProductSizesId",
            //    table: "Order Details");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_Order Details_Products_ProductsId",
            //    table: "Order Details");

            //migrationBuilder.DropIndex(
            //    name: "IX_Order Details_ProductSizesId",
            //    table: "Order Details");

            //migrationBuilder.DropIndex(
            //    name: "IX_Order Details_ProductsId",
            //    table: "Order Details");

            //migrationBuilder.DropColumn(
            //    name: "ProductSizesId",
            //    table: "Order Details");

            //migrationBuilder.DropColumn(
            //    name: "ProductsId",
            //    table: "Order Details");

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "ProductSizeId",
            //    table: "Order Details",
            //    nullable: false,
            //    oldClrType: typeof(int));

            //migrationBuilder.AlterColumn<Guid>(
            //    name: "ProductID",
            //    table: "Order Details",
            //    nullable: false,
            //    oldClrType: typeof(int));

            //migrationBuilder.CreateIndex(
            //    name: "IX_Order Details_ProductSizeId",
            //    table: "Order Details",
            //    column: "ProductSizeId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order_Details_Products",
            //    table: "Order Details",
            //    column: "ProductID",
            //    principalTable: "Products",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Order_Details_Products_sizes",
            //    table: "Order Details",
            //    column: "ProductSizeId",
            //    principalTable: "ProductSizes",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}
