using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class TestingSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "PermissionId", "CodeName", "Name" },
                values: new object[,]
                {
                    { 1L, "user.add", "Add User" },
                    { 2L, "user.edit", "Edit User" },
                    { 3L, "user.modify", "Modify User" },
                    { 4L, "user.delete", "Delete User" },
                    { 5L, "user.list", "List Users" },
                    { 6L, "role.add", "Add Role" },
                    { 7L, "role.edit", "Modify Role" },
                    { 8L, "role.list", "List Role" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Admin" },
                    { 2L, "Player" }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "RoleId", "PermissionID" },
                values: new object[,]
                {
                    { 1L, 1L },
                    { 1L, 2L },
                    { 1L, 3L },
                    { 1L, 4L },
                    { 1L, 5L },
                    { 1L, 6L },
                    { 1L, 7L },
                    { 1L, 8L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 2L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 3L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 4L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 5L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 6L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 7L });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "RoleId", "PermissionID" },
                keyValues: new object[] { 1L, 8L });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Permission",
                keyColumn: "PermissionId",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
