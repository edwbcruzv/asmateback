using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AhorrosPrestamosYMovimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AhorrosVoluntario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoInicial = table.Column<int>(type: "int", nullable: false),
                    PeriodoFinal = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Rendimiento = table.Column<float>(type: "real", nullable: false),
                    Descuento = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhorrosVoluntario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AhorrosVoluntario_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AhorrosVoluntario_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AhorrosWise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoInicial = table.Column<int>(type: "int", nullable: false),
                    PeriodoFinal = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Rendimiento = table.Column<float>(type: "real", nullable: false),
                    SrcFileConstancia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SrcFilePago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AhorrosWise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AhorrosWise_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AhorrosWise_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodoInicial = table.Column<int>(type: "int", nullable: false),
                    PeriodoFinal = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: false),
                    Plazo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    MontoPagado = table.Column<float>(type: "real", nullable: false),
                    Interes = table.Column<float>(type: "real", nullable: false),
                    TazaInteres = table.Column<float>(type: "real", nullable: false),
                    PlazoInteres = table.Column<float>(type: "real", nullable: false),
                    FondoGarantia = table.Column<float>(type: "real", nullable: false),
                    TazaFondoGarantia = table.Column<float>(type: "real", nullable: false),
                    FechaTransferencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descuento = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovimientosAhorrosVoluntario",
                columns: table => new
                {
                    AhorroVoluntarioId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    Rendimiento = table.Column<float>(type: "real", nullable: false),
                    EstadoTransaccion = table.Column<int>(type: "int", nullable: false),
                    Interes = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosAhorrosVoluntario", x => new { x.AhorroVoluntarioId, x.EmployeeId, x.CompanyId, x.MovimientoId });
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosVoluntario_AhorrosVoluntario_AhorroVoluntarioId",
                        column: x => x.AhorroVoluntarioId,
                        principalTable: "AhorrosVoluntario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosVoluntario_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosVoluntario_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovimientosAhorrosWise",
                columns: table => new
                {
                    AhorroWiseId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    Rendimiento = table.Column<float>(type: "real", nullable: false),
                    EstadoTransaccion = table.Column<int>(type: "int", nullable: false),
                    Interes = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosAhorrosWise", x => new { x.AhorroWiseId, x.EmployeeId, x.CompanyId, x.MovimientoId });
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosWise_AhorrosWise_AhorroWiseId",
                        column: x => x.AhorroWiseId,
                        principalTable: "AhorrosWise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosWise_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosAhorrosWise_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovimientosPrestamo",
                columns: table => new
                {
                    PrestamoId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    MovimientoId = table.Column<int>(type: "int", nullable: false),
                    Periodo = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    Rendimiento = table.Column<float>(type: "real", nullable: false),
                    EstadoTransaccion = table.Column<int>(type: "int", nullable: false),
                    Capital = table.Column<float>(type: "real", nullable: false),
                    FondoGarantia = table.Column<float>(type: "real", nullable: false),
                    SaldoActual = table.Column<float>(type: "real", nullable: false),
                    Interes = table.Column<float>(type: "real", nullable: false),
                    Moratorio = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosPrestamo", x => new { x.PrestamoId, x.EmployeeId, x.CompanyId, x.MovimientoId });
                    table.ForeignKey(
                        name: "FK_MovimientosPrestamo_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosPrestamo_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MovimientosPrestamo_Prestamos_PrestamoId",
                        column: x => x.PrestamoId,
                        principalTable: "Prestamos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AhorrosVoluntario_CompanyId",
                table: "AhorrosVoluntario",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AhorrosVoluntario_EmployeeId",
                table: "AhorrosVoluntario",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AhorrosWise_CompanyId",
                table: "AhorrosWise",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AhorrosWise_EmployeeId",
                table: "AhorrosWise",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAhorrosVoluntario_CompanyId",
                table: "MovimientosAhorrosVoluntario",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAhorrosVoluntario_EmployeeId",
                table: "MovimientosAhorrosVoluntario",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAhorrosWise_CompanyId",
                table: "MovimientosAhorrosWise",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosAhorrosWise_EmployeeId",
                table: "MovimientosAhorrosWise",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosPrestamo_CompanyId",
                table: "MovimientosPrestamo",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosPrestamo_EmployeeId",
                table: "MovimientosPrestamo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_CompanyId",
                table: "Prestamos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_EmployeeId",
                table: "Prestamos",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosAhorrosVoluntario");

            migrationBuilder.DropTable(
                name: "MovimientosAhorrosWise");

            migrationBuilder.DropTable(
                name: "MovimientosPrestamo");

            migrationBuilder.DropTable(
                name: "AhorrosVoluntario");

            migrationBuilder.DropTable(
                name: "AhorrosWise");

            migrationBuilder.DropTable(
                name: "Prestamos");
        }
    }
}
