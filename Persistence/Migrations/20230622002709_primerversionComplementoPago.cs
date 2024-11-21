using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class primerversionComplementoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplementoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    LogoSrcCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LugarExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    ReceptorRfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorDomicilioFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<int>(type: "int", nullable: true),
                    Estatus = table.Column<short>(type: "smallint", nullable: false),
                    FechaTimbrado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelloCfdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelloSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CadenaOriginal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoCertificadoSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotivoCancelacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PagoSrcPdf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormaPagoId = table.Column<int>(type: "int", nullable: false),
                    TipoMonedaId = table.Column<int>(type: "int", nullable: false),
                    FileXmlTimbrado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplementoPagos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_complementopago_copamany",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_complementopago_emisorregimenfiscal",
                        column: x => x.EmisorRegimenFiscalId,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_complementopago_formapago",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_complementopago_tipomoneda",
                        column: x => x.TipoMonedaId,
                        principalTable: "TipoMonedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacturasAsociadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplementoPagoId = table.Column<int>(type: "int", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturasAsociadas", x => x.Id);
                    table.ForeignKey(
                        name: "fk_facturaasociada_factura",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_facturasasociada_complementopago",
                        column: x => x.ComplementoPagoId,
                        principalTable: "ComplementoPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_CompanyId",
                table: "ComplementoPagos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_EmisorRegimenFiscalId",
                table: "ComplementoPagos",
                column: "EmisorRegimenFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_FormaPagoId",
                table: "ComplementoPagos",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_TipoMonedaId",
                table: "ComplementoPagos",
                column: "TipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasAsociadas_ComplementoPagoId",
                table: "FacturasAsociadas",
                column: "ComplementoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasAsociadas_FacturaId",
                table: "FacturasAsociadas",
                column: "FacturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturasAsociadas");

            migrationBuilder.DropTable(
                name: "ComplementoPagos");
        }
    }
}
