using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class LimiteModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Limites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateDemande = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LimiteAssurance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LimiteFinancement = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DateLimite = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateDerniereDemande = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DelaiDemande = table.Column<int>(type: "int", nullable: false),
                    ModePaiement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limites_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Limites_ContratId",
                table: "Limites",
                column: "ContratId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Limites");
        }
    }
}
