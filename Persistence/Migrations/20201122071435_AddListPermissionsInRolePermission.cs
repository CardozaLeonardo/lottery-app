using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddListPermissionsInRolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "CodeName", "Name" },
                values: new object[] { 9L, "role.permission.list", "List Permissions in Role" });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "RoleId", "PermissionID" },
                values: new object[] { 1L, 9L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 9L });

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 9L);
        }
    }
}
