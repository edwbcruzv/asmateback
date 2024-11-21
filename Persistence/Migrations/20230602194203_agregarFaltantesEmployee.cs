using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class agregarFaltantesEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormaPagoId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoJornadaId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPeriocidadPagoId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoPrevicionSocial",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Periodos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Etapa = table.Column<int>(type: "int", nullable: false),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Asistencias = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_CompanyId_Periodos",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoJornada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoJornada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPeriocidadPago",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPeriocidadPago", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_FormaPagoId",
                table: "Employees",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TipoJornadaId",
                table: "Employees",
                column: "TipoJornadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TipoPeriocidadPagoId",
                table: "Employees",
                column: "TipoPeriocidadPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Periodos_CompanyId",
                table: "Periodos",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_FormaPagos_FormaPagoId",
                table: "Employees",
                column: "FormaPagoId",
                principalTable: "FormaPagos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_TipoPeriocidadPago_TipoPeriocidadPagoId",
                table: "Employees",
                column: "TipoPeriocidadPagoId",
                principalTable: "TipoPeriocidadPago",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoJornada_employee",
                table: "Employees",
                column: "TipoJornadaId",
                principalTable: "TipoJornada",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_FormaPagos_FormaPagoId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_TipoPeriocidadPago_TipoPeriocidadPagoId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoJornada_employee",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Periodos");

            migrationBuilder.DropTable(
                name: "TipoJornada");

            migrationBuilder.DropTable(
                name: "TipoPeriocidadPago");

            migrationBuilder.DropIndex(
                name: "IX_Employees_FormaPagoId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TipoJornadaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TipoPeriocidadPagoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TipoJornadaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TipoPeriocidadPagoId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TipoPrevicionSocial",
                table: "Employees");
        }
    }
}
