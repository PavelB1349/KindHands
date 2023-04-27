using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindHands.Migrations
{
    public partial class AddUserAdminModerator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsModerator",
                schema: "dbo",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "dbo",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsModerator",
                schema: "dbo",
                table: "User");
        }
    }
}
