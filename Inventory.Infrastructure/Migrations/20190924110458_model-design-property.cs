using Microsoft.EntityFrameworkCore.Migrations;

namespace Inventory.Persistance.Migrations
{
    public partial class modeldesignproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "ModelDesign",
                newName: "Sizes");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ModelDesign",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Cost",
                table: "ModelDesign",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "ModelDesign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "ModelDesign",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manifature",
                table: "ModelDesign",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SalesPrice",
                table: "ModelDesign",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "ModelDesign");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ModelDesign");

            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "ModelDesign");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "ModelDesign");

            migrationBuilder.DropColumn(
                name: "Manifature",
                table: "ModelDesign");

            migrationBuilder.DropColumn(
                name: "SalesPrice",
                table: "ModelDesign");

            migrationBuilder.RenameColumn(
                name: "Sizes",
                table: "ModelDesign",
                newName: "Data");
        }
    }
}
