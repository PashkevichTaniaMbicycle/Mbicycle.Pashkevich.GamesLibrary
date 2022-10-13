using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesLib.DataAccess.Migrations
{
    public partial class AddForeignKeysToGame : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Games_DevId",
                table: "Games",
                column: "DevId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PublisherId",
                table: "Games",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Devs_DevId",
                table: "Games",
                column: "DevId",
                principalTable: "Devs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Devs_DevId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_Publishers_PublisherId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_DevId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_PublisherId",
                table: "Games");
        }
    }
}
