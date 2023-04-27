using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindHands.Migrations
{
    public partial class AddPhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dbo",
                table: "VolunterAnnouncement",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                schema: "dbo",
                table: "Veterinarian",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dbo",
                table: "VolunterAnnouncement");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                schema: "dbo",
                table: "Veterinarian");
        }
    }
}
