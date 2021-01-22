using Microsoft.EntityFrameworkCore.Migrations;

namespace TMS.Migrations
{
    public partial class teacer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Student",
                newName: "RoleType");

            migrationBuilder.AddColumn<string>(
                name: "RoleType",
                table: "Teacher",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleTypeId",
                table: "Teacher",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoleTypeId",
                table: "Student",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleType",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "RoleTypeId",
                table: "Teacher");

            migrationBuilder.DropColumn(
                name: "RoleTypeId",
                table: "Student");

            migrationBuilder.RenameColumn(
                name: "RoleType",
                table: "Student",
                newName: "Role");
        }
    }
}
