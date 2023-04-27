using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindHands.Migrations
{
    public partial class AnimaltestMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Shelter_ShelterId",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                schema: "dbo",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "ShelterId",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                schema: "dbo",
                table: "Animal",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "dbo",
                table: "Animal",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Passport",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sex",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Shelter_ShelterId",
                schema: "dbo",
                table: "Animal",
                column: "ShelterId",
                principalSchema: "dbo",
                principalTable: "Shelter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Shelter_ShelterId",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Age",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Passport",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "Sex",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                schema: "dbo",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ShelterId",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Breed",
                schema: "dbo",
                table: "Animal",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_Shelter_ShelterId",
                schema: "dbo",
                table: "Animal",
                column: "ShelterId",
                principalSchema: "dbo",
                principalTable: "Shelter",
                principalColumn: "Id");
        }
    }
}
