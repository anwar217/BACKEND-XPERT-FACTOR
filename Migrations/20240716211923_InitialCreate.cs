using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contrats",
                columns: table => new
                {
                    ContratId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReferenceContrat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MontantContrat = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrats", x => x.ContratId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Individus",
                columns: table => new
                {
                    IndividuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Prenom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individus", x => x.IndividuId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bordereaux",
                columns: table => new
                {
                    BordereauId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MontantTotal = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DateBordereau = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NombreDocuments = table.Column<int>(type: "int", nullable: false),
                    AnneeBordereau = table.Column<int>(type: "int", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bordereaux", x => x.BordereauId);
                    table.ForeignKey(
                        name: "FK_Bordereaux_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IndividuContrats",
                columns: table => new
                {
                    IndividuId = table.Column<int>(type: "int", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IndividuContrats", x => new { x.IndividuId, x.ContratId });
                    table.ForeignKey(
                        name: "FK_IndividuContrats_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IndividuContrats_Individus_IndividuId",
                        column: x => x.IndividuId,
                        principalTable: "Individus",
                        principalColumn: "IndividuId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Factures",
                columns: table => new
                {
                    FactureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MontantDocument = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    RefFacture = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Echeance = table.Column<int>(type: "int", nullable: false),
                    DateFacture = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModeReglement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BordereauId = table.Column<int>(type: "int", nullable: false),
                    IndividuId = table.Column<int>(type: "int", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Factures", x => x.FactureId);
                    table.ForeignKey(
                        name: "FK_Factures_Bordereaux_BordereauId",
                        column: x => x.BordereauId,
                        principalTable: "Bordereaux",
                        principalColumn: "BordereauId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factures_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Factures_Individus_IndividuId",
                        column: x => x.IndividuId,
                        principalTable: "Individus",
                        principalColumn: "IndividuId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bordereaux_ContratId",
                table: "Bordereaux",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_BordereauId",
                table: "Factures",
                column: "BordereauId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_ContratId",
                table: "Factures",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Factures_IndividuId",
                table: "Factures",
                column: "IndividuId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividuContrats_ContratId",
                table: "IndividuContrats",
                column: "ContratId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "IndividuContrats");

            migrationBuilder.DropTable(
                name: "Bordereaux");

            migrationBuilder.DropTable(
                name: "Individus");

            migrationBuilder.DropTable(
                name: "Contrats");
        }
    }
}
