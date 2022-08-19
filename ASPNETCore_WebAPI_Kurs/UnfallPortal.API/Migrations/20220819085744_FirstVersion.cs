using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnfallPortal.API.Migrations
{
    public partial class FirstVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErsteHilfeKurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErsteHilfeKurs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mandant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MandantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mandant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mandant_Mandant_MandantId",
                        column: x => x.MandantId,
                        principalTable: "Mandant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MandantKurs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MandantId = table.Column<int>(type: "int", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    ErsteHilfeKursId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandantKurs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandantKurs_ErsteHilfeKurs_ErsteHilfeKursId",
                        column: x => x.ErsteHilfeKursId,
                        principalTable: "ErsteHilfeKurs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MandantKurs_Mandant_MandantId",
                        column: x => x.MandantId,
                        principalTable: "Mandant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MandantUnfall",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MandantId = table.Column<int>(type: "int", nullable: false),
                    UnfallName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandantUnfall", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandantUnfall_Mandant_MandantId",
                        column: x => x.MandantId,
                        principalTable: "Mandant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MandantUnfallDokumente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MandantUnfallId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dateinamen = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MandantUnfallDokumente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MandantUnfallDokumente_MandantUnfall_MandantUnfallId",
                        column: x => x.MandantUnfallId,
                        principalTable: "MandantUnfall",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mandant_MandantId",
                table: "Mandant",
                column: "MandantId");

            migrationBuilder.CreateIndex(
                name: "IX_MandantKurs_ErsteHilfeKursId",
                table: "MandantKurs",
                column: "ErsteHilfeKursId");

            migrationBuilder.CreateIndex(
                name: "IX_MandantKurs_MandantId",
                table: "MandantKurs",
                column: "MandantId");

            migrationBuilder.CreateIndex(
                name: "IX_MandantUnfall_MandantId",
                table: "MandantUnfall",
                column: "MandantId");

            migrationBuilder.CreateIndex(
                name: "IX_MandantUnfallDokumente_MandantUnfallId",
                table: "MandantUnfallDokumente",
                column: "MandantUnfallId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MandantKurs");

            migrationBuilder.DropTable(
                name: "MandantUnfallDokumente");

            migrationBuilder.DropTable(
                name: "ErsteHilfeKurs");

            migrationBuilder.DropTable(
                name: "MandantUnfall");

            migrationBuilder.DropTable(
                name: "Mandant");
        }
    }
}
