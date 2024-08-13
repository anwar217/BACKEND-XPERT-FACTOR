using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class LitigeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Litiges",
                columns: table => new
                {
                    LitigeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeDuLitige = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateLitige = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateEcheanceLitige = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false),
                    FactureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Litiges", x => x.LitigeId);
                    table.ForeignKey(
                        name: "FK_Litiges_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Litiges_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "FactureId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_ContratId",
                table: "Litiges",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_FactureId",
                table: "Litiges",
                column: "FactureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Litiges");
        }
    }
}
