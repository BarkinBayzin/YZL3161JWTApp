using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAppBackOffice.Persistance.Migrations
{
    public partial class ProductPropsDataChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Definition",
                table: "Products",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Definition");
        }
    }
}
