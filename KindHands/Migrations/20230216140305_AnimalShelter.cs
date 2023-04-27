using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KindHands.Migrations
{
    public partial class AnimalShelter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_Shelter_ShelterId",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Animal_ShelterId",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.DropColumn(
                name: "ShelterId",
                schema: "dbo",
                table: "Animal");

            migrationBuilder.CreateTable(
                name: "AnimalsShelters",
                schema: "dbo",
                columns: table => new
                {
                    AnimalsId = table.Column<int>(type: "int", nullable: false),
                    SheltersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalsShelters", x => new { x.AnimalsId, x.SheltersId });
                    table.ForeignKey(
                        name: "FK_AnimalsShelters_Animal_AnimalsId",
                        column: x => x.AnimalsId,
                        principalSchema: "dbo",
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalsShelters_Shelter_SheltersId",
                        column: x => x.SheltersId,
                        principalSchema: "dbo",
                        principalTable: "Shelter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalsShelters_SheltersId",
                schema: "dbo",
                table: "AnimalsShelters",
                column: "SheltersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalsShelters",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "ShelterId",
                schema: "dbo",
                table: "Animal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Animal_ShelterId",
                schema: "dbo",
                table: "Animal",
                column: "ShelterId");

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
    }
}
