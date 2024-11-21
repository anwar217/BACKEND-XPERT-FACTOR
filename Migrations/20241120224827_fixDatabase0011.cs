using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class fixDatabase0011 : Migration
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
                    MontantContrat = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    StatutContrat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeContrat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DeviceContrat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FondGarantie = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FraisCreation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FraisLimite = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateDebContrat = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateSignContrat = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumberPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false)
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
                    Statut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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
                name: "Disponibles",
                columns: table => new
                {
                    DisponibleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Formule = table.Column<int>(type: "int", nullable: false),
                    FactureEnCours = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FondsDeGaranties = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    FondsDeReserve = table.Column<int>(type: "int", nullable: false),
                    DepassementLimiteFinancementAcheteurs = table.Column<int>(type: "int", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disponibles", x => x.DisponibleId);
                    table.ForeignKey(
                        name: "FK_Disponibles_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Financements",
                columns: table => new
                {
                    FinancementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeDeFinancement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MontantFinancement = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DateDeFinancement = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StatutFinancement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MethodeDePaiement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContratId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Financements", x => x.FinancementId);
                    table.ForeignKey(
                        name: "FK_Financements_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Limites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateDemande = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LimiteAssurance = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    LimiteFinancement = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateLimite = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DateDerniereDemande = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DelaiDemande = table.Column<int>(type: "int", nullable: false),
                    ModePaiement = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
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
                    Status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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

            migrationBuilder.CreateTable(
                name: "Litiges",
                columns: table => new
                {
                    LitigeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeDuLitige = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateLitige = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Statut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
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

            migrationBuilder.CreateTable(
                name: "Prorogations",
                columns: table => new
                {
                    ProrogationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Echeance = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MotifProrogation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Statut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateEcheanceApresProrogation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ContratId = table.Column<int>(type: "int", nullable: false),
                    FactureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prorogations", x => x.ProrogationId);
                    table.ForeignKey(
                        name: "FK_Prorogations_Contrats_ContratId",
                        column: x => x.ContratId,
                        principalTable: "Contrats",
                        principalColumn: "ContratId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prorogations_Factures_FactureId",
                        column: x => x.FactureId,
                        principalTable: "Factures",
                        principalColumn: "FactureId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Bordereaux_ContratId",
                table: "Bordereaux",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Disponibles_ContratId",
                table: "Disponibles",
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
                name: "IX_Financements_ContratId",
                table: "Financements",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_IndividuContrats_ContratId",
                table: "IndividuContrats",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Limites_ContratId",
                table: "Limites",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_ContratId",
                table: "Litiges",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Litiges_FactureId",
                table: "Litiges",
                column: "FactureId");

            migrationBuilder.CreateIndex(
                name: "IX_Prorogations_ContratId",
                table: "Prorogations",
                column: "ContratId");

            migrationBuilder.CreateIndex(
                name: "IX_Prorogations_FactureId",
                table: "Prorogations",
                column: "FactureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disponibles");

            migrationBuilder.DropTable(
                name: "Financements");

            migrationBuilder.DropTable(
                name: "IndividuContrats");

            migrationBuilder.DropTable(
                name: "Limites");

            migrationBuilder.DropTable(
                name: "Litiges");

            migrationBuilder.DropTable(
                name: "Prorogations");

            migrationBuilder.DropTable(
                name: "Factures");

            migrationBuilder.DropTable(
                name: "Bordereaux");

            migrationBuilder.DropTable(
                name: "Individus");

            migrationBuilder.DropTable(
                name: "Contrats");
        }
    }
}
