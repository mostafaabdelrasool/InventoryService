using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class addconcurecy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Products",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Orders");
        }
    }
}
