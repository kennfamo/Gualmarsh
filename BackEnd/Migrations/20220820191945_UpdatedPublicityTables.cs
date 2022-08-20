using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class UpdatedPublicityTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPublicity_Province_CategoryId",
                table: "ProductPublicity");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductPublicity",
                newName: "ProductSubcategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPublicity_CategoryId",
                table: "ProductPublicity",
                newName: "IX_ProductPublicity_ProductSubcategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPublicity_ProductSubcategory_ProductSubcategoryId",
                table: "ProductPublicity",
                column: "ProductSubcategoryId",
                principalTable: "ProductSubcategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPublicity_ProductSubcategory_ProductSubcategoryId",
                table: "ProductPublicity");

            migrationBuilder.RenameColumn(
                name: "ProductSubcategoryId",
                table: "ProductPublicity",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPublicity_ProductSubcategoryId",
                table: "ProductPublicity",
                newName: "IX_ProductPublicity_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPublicity_Province_CategoryId",
                table: "ProductPublicity",
                column: "CategoryId",
                principalTable: "Province",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
