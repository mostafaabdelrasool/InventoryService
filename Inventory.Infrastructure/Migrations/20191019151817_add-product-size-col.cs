using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class addproductsizecol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "ProductSizes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteDate",
                table: "ProductSizes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductSizes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "ProductSizes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "DeleteDate",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "ProductSizes");
        }
    }
}
