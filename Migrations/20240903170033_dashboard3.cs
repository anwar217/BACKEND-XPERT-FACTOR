using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class dashboard3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DepassementLimiteFinancementAcheteursDisponible1",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "DepassementLimiteFinancementAcheteursDisponible2",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FactureEnCoursDisponible1",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FactureEnCoursDisponible2",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FondsDeGarantiesDisponible1",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FondsDeGarantiesDisponible2",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FondsDeReserveDisponible1",
                table: "Contrats");

            migrationBuilder.DropColumn(
                name: "FondsDeReserveDisponible2",
                table: "Contrats");

            migrationBuilder.CreateTable(
                name: "Disponibles",
                columns: table => new
                {
                    DisponibleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Formule = table.Column<int>(type: "int", nullable: false),
                    FactureEnCours = table.Column<int>(type: "int", nullable: false),
                    FondsDeGaranties = table.Column<int>(type: "int", nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_Disponibles_ContratId",
                table: "Disponibles",
                column: "ContratId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Disponibles");

            migrationBuilder.AddColumn<int>(
                name: "DepassementLimiteFinancementAcheteursDisponible1",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepassementLimiteFinancementAcheteursDisponible2",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FactureEnCoursDisponible1",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FactureEnCoursDisponible2",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FondsDeGarantiesDisponible1",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FondsDeGarantiesDisponible2",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FondsDeReserveDisponible1",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FondsDeReserveDisponible2",
                table: "Contrats",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
