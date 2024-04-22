using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_CMC.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "canView",
                table: "AspNetRoles",
                newName: "CanView");

            migrationBuilder.RenameColumn(
                name: "canEdit",
                table: "AspNetRoles",
                newName: "CanEdit");

            migrationBuilder.RenameColumn(
                name: "canDelete",
                table: "AspNetRoles",
                newName: "CanDelete");

            migrationBuilder.AddColumn<bool>(
                name: "CanAddUser",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanChangeUserRole",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanDeleteUser",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanEditUser",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanExport",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanImport",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanPrint",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewUser",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanAddUser",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanChangeUserRole",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanDeleteUser",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanEditUser",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanExport",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanImport",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanPrint",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CanViewUser",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "CanView",
                table: "AspNetRoles",
                newName: "canView");

            migrationBuilder.RenameColumn(
                name: "CanEdit",
                table: "AspNetRoles",
                newName: "canEdit");

            migrationBuilder.RenameColumn(
                name: "CanDelete",
                table: "AspNetRoles",
                newName: "canDelete");
        }
    }
}
