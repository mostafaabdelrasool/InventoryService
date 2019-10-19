using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class changecategorynamesize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 15);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
