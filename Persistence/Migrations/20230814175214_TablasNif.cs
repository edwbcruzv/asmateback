using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TablasNif : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nif",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEjercicio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nif", x => x.Id);
                    table.ForeignKey(
                        name: "fk_company_nif",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NifResultado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rfc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdEmpleado = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SueldoDiario = table.Column<double>(type: "float", nullable: false),
                    SueldoBase = table.Column<double>(type: "float", nullable: false),
                    FechaIngreso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Isr = table.Column<double>(type: "float", nullable: false),
                    CuotasPatronales = table.Column<double>(type: "float", nullable: false),
                    PrimaVacacional = table.Column<double>(type: "float", nullable: false),
                    Aguinaldo = table.Column<double>(type: "float", nullable: false),
                    NifId = table.Column<int>(type: "int", nullable: false),
                    Subsidio = table.Column<double>(type: "float", nullable: false),
                    Vacaciones = table.Column<double>(type: "float", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Año = table.Column<int>(type: "int", nullable: false),
                    Rcv = table.Column<double>(type: "float", nullable: false),
                    Infonavit = table.Column<double>(type: "float", nullable: false),
                    PrimaAntiguedad = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NifResultado", x => x.Id);
                    table.ForeignKey(
                        name: "fk_nif_nifResultados",
                        column: x => x.NifId,
                        principalTable: "Nif",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nif_CompanyId",
                table: "Nif",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_NifResultado_NifId",
                table: "NifResultado",
                column: "NifId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NifResultado");

            migrationBuilder.DropTable(
                name: "Nif");
        }
    }
}
