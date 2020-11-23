using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AnotherTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Winner_PlayerRaffleId",
                table: "Winner");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_PlayerRaffleId",
                table: "Winner",
                column: "PlayerRaffleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Winner_PlayerRaffleId",
                table: "Winner");

            migrationBuilder.CreateIndex(
                name: "IX_Winner_PlayerRaffleId",
                table: "Winner",
                column: "PlayerRaffleId");
        }
    }
}
