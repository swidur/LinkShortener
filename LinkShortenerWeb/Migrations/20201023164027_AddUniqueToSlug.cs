using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkShortenerWeb.Migrations
{
    public partial class AddUniqueToSlug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Shortening_Slug",
                table: "Shortening",
                column: "Slug",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Shortening_Slug",
                table: "Shortening");
        }
    }
}
