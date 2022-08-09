using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class AddedSiteIdOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_Site_SiteId",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_SiteId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "OrderHeader");
        }
    }
}
