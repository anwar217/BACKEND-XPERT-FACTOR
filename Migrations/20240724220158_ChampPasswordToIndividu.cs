﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace factoring1.Migrations
{
    /// <inheritdoc />
    public partial class ChampPasswordToIndividu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Individus",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Individus");
        }
    }
}
