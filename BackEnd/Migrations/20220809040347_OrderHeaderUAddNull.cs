using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class OrderHeaderUAddNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
               name: "UserAddressId",
               table: "OrderHeader",
               nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
