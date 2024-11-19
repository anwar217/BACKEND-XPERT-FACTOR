using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class modifProrogation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeProrogation",
                table: "Prorogations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeProrogation",
                table: "Prorogations",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
