using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoFechaPagoReembolso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SrcPdfFichaPago",
                table: "Rembolsos");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPago",
                table: "Rembolsos",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaPago",
                table: "Rembolsos");

            migrationBuilder.AddColumn<string>(
                name: "SrcPdfFichaPago",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
