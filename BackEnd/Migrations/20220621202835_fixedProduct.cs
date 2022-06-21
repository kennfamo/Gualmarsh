using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class fixedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiscountCode",
                table: "Product",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountCode",
                table: "Product",
                column: "DiscountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Discount_DiscountCode",
                table: "Product",
                column: "DiscountCode",
                principalTable: "Discount",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Discount_DiscountCode",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DiscountCode",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DiscountCode",
                table: "Product");
        }
    }
}
