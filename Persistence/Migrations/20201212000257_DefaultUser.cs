using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class DefaultUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Username" },
                values: new object[] { 100L, "jorgeProfe@gmail.com", "Morales", "Jorge", "$2a$11$tBDwzGx2ogUlt826DdJ6ouWFwbeZdU.x.8tL1xGlIg7B/T4cNfQMG", "jorginho777" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Identification", "UserId" },
                values: new object[] { 100L, "090-250989-1006U", 100L });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { 100L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { 100L, 1L });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 100L);
        }
    }
}
