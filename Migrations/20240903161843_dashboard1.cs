using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class dashboard1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
