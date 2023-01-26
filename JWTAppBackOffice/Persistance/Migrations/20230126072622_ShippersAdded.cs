using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JWTAppBackOffice.Persistance.Migrations
{
    public partial class ShippersAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shipper_ShipperId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shipper",
                table: "Shipper");

            migrationBuilder.RenameTable(
                name: "Shipper",
                newName: "Shippers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shippers_ShipperId",
                table: "Products",
                column: "ShipperId",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Shippers_ShipperId",
                table: "Products");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Shippers",
                table: "Shippers");

            migrationBuilder.RenameTable(
                name: "Shippers",
                newName: "Shipper");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shipper",
                table: "Shipper",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Shipper_ShipperId",
                table: "Products",
                column: "ShipperId",
                principalTable: "Shipper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
