using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormattersSampleWebAPI.Migrations
{
    public partial class FirstV : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FootballClub",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundationYear = table.Column<int>(type: "int", nullable: false),
                    Members = table.Column<int>(type: "int", nullable: false),
                    Division = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FootballClub", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FootballClub");
        }
    }
}
