using Microsoft.EntityFrameworkCore.Migrations;

namespace Product.Persistance.Migrations
{
    public partial class increaseproductname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSizes_Products_ProductsId",
                table: "ProductSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_ProductsId",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "ProductsId",
                table: "ProductSizes");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsId",
                table: "ProductSizes",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductsId",
                table: "ProductSizes",
                column: "ProductsId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSizes_Products_ProductsId",
                table: "ProductSizes",
                column: "ProductsId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
