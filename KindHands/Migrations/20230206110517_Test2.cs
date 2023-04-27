using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindHands.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dbo",
                table: "Shelter",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dbo",
                table: "Clinic",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dbo",
                table: "Shelter");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dbo",
                table: "Clinic");
        }
    }
}
