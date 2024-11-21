using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tablesNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoDeducciones",
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
                    table.PrimaryKey("PK_TipoDeducciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPercepciones",
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
                    table.PrimaryKey("PK_TipoPercepciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nominas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    EmisorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRegimenFistalId = table.Column<int>(type: "int", nullable: false),
                    LogoSrcCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LugarExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReceptorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    ReceptorUsoCfdiId = table.Column<int>(type: "int", nullable: false),
                    ReceptorDomicilioFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoMonedaId = table.Column<int>(type: "int", nullable: false),
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false),
                    TipoPeriocidadPagoId = table.Column<int>(type: "int", nullable: false),
                    PuestoId = table.Column<int>(type: "int", nullable: false),
                    TipoContratoId = table.Column<int>(type: "int", nullable: false),
                    TipoJornadaId = table.Column<int>(type: "int", nullable: false),
                    TipoRegimenId = table.Column<int>(type: "int", nullable: false),
                    TipoRiesgoTrabajoId = table.Column<int>(type: "int", nullable: false),
                    PeriodoId = table.Column<int>(type: "int", nullable: false),
                    Desde = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hasta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaTimbrado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelloCfdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelloSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CadenaOriginal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoCertificadoSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileXmlTimbrado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estatus = table.Column<short>(type: "smallint", nullable: false),
                    TipoNomina = table.Column<short>(type: "smallint", nullable: false),
                    Folio = table.Column<int>(type: "int", nullable: false),
                    TipoDeduccionId = table.Column<int>(type: "int", nullable: true),
                    TipoPercepcionId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Nominas_TipoDeducciones_TipoDeduccionId",
                        column: x => x.TipoDeduccionId,
                        principalTable: "TipoDeducciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Nominas_TipoPercepciones_TipoPercepcionId",
                        column: x => x.TipoPercepcionId,
                        principalTable: "TipoPercepciones",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_MetodoPago_nominas",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodoPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Periodo_nominas",
                        column: x => x.PeriodoId,
                        principalTable: "Periodos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_Puesto_nominas",
                        column: x => x.PuestoId,
                        principalTable: "Puestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_TipoContrato_nominas",
                        column: x => x.TipoContratoId,
                        principalTable: "TipoContratos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_TipoJornada_nominas",
                        column: x => x.TipoJornadaId,
                        principalTable: "TipoJornada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_TipoPeriocidadPago_nominas",
                        column: x => x.TipoPeriocidadPagoId,
                        principalTable: "TipoPeriocidadPago",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_TipoRegimen_nominas",
                        column: x => x.TipoRegimenId,
                        principalTable: "TipoRegimens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_TipoRiesgoTrabajo_nominas",
                        column: x => x.TipoRiesgoTrabajoId,
                        principalTable: "TipoRiesgoTrabajos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_UsoCfdi_nominas",
                        column: x => x.ReceptorUsoCfdiId,
                        principalTable: "UsoCfdis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_company_nominas",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_employee_nominas",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_regimenFiscal_nominas",
                        column: x => x.EmisorRegimenFistalId,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NominaDeducciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaDeducciones", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Nomina_NominaDeducciones",
                        column: x => x.NominaId,
                        principalTable: "Nominas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NominaOtroPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Importe = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaOtroPagos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Nomina_NominaOtroPagos",
                        column: x => x.NominaId,
                        principalTable: "Nominas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NominaPercepciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NominaId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImporteGravado = table.Column<double>(type: "float", nullable: false),
                    ImporteExento = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominaPercepciones", x => x.Id);
                    table.ForeignKey(
                        name: "fk_Nomina_NominaPercepciones",
                        column: x => x.NominaId,
                        principalTable: "Nominas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NominaDeducciones_NominaId",
                table: "NominaDeducciones",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaOtroPagos_NominaId",
                table: "NominaOtroPagos",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominaPercepciones_NominaId",
                table: "NominaPercepciones",
                column: "NominaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_CompanyId",
                table: "Nominas",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_EmisorRegimenFistalId",
                table: "Nominas",
                column: "EmisorRegimenFistalId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_EmployeeId",
                table: "Nominas",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_MetodoPagoId",
                table: "Nominas",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_PeriodoId",
                table: "Nominas",
                column: "PeriodoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_PuestoId",
                table: "Nominas",
                column: "PuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_ReceptorUsoCfdiId",
                table: "Nominas",
                column: "ReceptorUsoCfdiId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoContratoId",
                table: "Nominas",
                column: "TipoContratoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoDeduccionId",
                table: "Nominas",
                column: "TipoDeduccionId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoJornadaId",
                table: "Nominas",
                column: "TipoJornadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoPercepcionId",
                table: "Nominas",
                column: "TipoPercepcionId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoPeriocidadPagoId",
                table: "Nominas",
                column: "TipoPeriocidadPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoRegimenId",
                table: "Nominas",
                column: "TipoRegimenId");

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoRiesgoTrabajoId",
                table: "Nominas",
                column: "TipoRiesgoTrabajoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NominaDeducciones");

            migrationBuilder.DropTable(
                name: "NominaOtroPagos");

            migrationBuilder.DropTable(
                name: "NominaPercepciones");

            migrationBuilder.DropTable(
                name: "Nominas");

            migrationBuilder.DropTable(
                name: "TipoDeducciones");

            migrationBuilder.DropTable(
                name: "TipoPercepciones");
        }
    }
}
