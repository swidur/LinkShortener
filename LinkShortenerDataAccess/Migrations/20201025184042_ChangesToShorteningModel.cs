using Microsoft.EntityFrameworkCore.Migrations;

namespace LinkShortenerDataAccess.Migrations
{
    public partial class ChangesToShorteningModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Shortening",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Shortening",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Shortening_UserId",
                table: "Shortening",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shortening_AspNetUsers_UserId",
                table: "Shortening",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shortening_AspNetUsers_UserId",
                table: "Shortening");

            migrationBuilder.DropIndex(
                name: "IX_Shortening_UserId",
                table: "Shortening");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Shortening");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Shortening");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
