using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    public partial class AddHelpfulReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Helpful",
                table: "Review");

            migrationBuilder.CreateTable(
                name: "HelpfulReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpfulReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpfulReview_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HelpfulReview_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HelpfulReview_ApplicationUserId",
                table: "HelpfulReview",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpfulReview_ReviewId",
                table: "HelpfulReview",
                column: "ReviewId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HelpfulReview");

            migrationBuilder.AddColumn<int>(
                name: "Helpful",
                table: "Review",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
