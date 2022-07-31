using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class orderHeaderModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_UserPayment_UserPaymentId",
                table: "OrderHeader");

            migrationBuilder.DropIndex(
                name: "IX_OrderHeader_UserPaymentId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Tax",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "UserPaymentId",
                table: "OrderHeader");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "Shipping",
                table: "OrderHeader",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "OrderHeader");

            migrationBuilder.AddColumn<decimal>(
                name: "Tax",
                table: "OrderHeader",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "UserPaymentId",
                table: "OrderHeader",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderHeader_UserPaymentId",
                table: "OrderHeader",
                column: "UserPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_UserPayment_UserPaymentId",
                table: "OrderHeader",
                column: "UserPaymentId",
                principalTable: "UserPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
