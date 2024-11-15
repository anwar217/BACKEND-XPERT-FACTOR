using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class addStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "Prorogations",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "Litiges",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "Limites",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "Financements",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "statut",
                table: "Bordereaux",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "statut",
                table: "Prorogations");

            migrationBuilder.DropColumn(
                name: "statut",
                table: "Litiges");

            migrationBuilder.DropColumn(
                name: "statut",
                table: "Limites");

            migrationBuilder.DropColumn(
                name: "statut",
                table: "Financements");

            migrationBuilder.DropColumn(
                name: "statut",
                table: "Bordereaux");
        }
    }
}
