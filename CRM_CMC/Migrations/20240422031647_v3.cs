using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_CMC.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "canDelete",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "canEdit",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "canView",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "canDelete",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "canEdit",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "canView",
                table: "AspNetRoles");
        }
    }
}
