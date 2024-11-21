using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tableFactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facturas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ReceptorRfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LugarExpedicion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsoCfdiId = table.Column<int>(type: "int", nullable: false),
                    FormaPagoId = table.Column<int>(type: "int", nullable: false),
                    FechaTimbrado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelloCfdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SelloSat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CadenaOriginal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estatus = table.Column<short>(type: "smallint", nullable: true),
                    TipoMonedaId = table.Column<int>(type: "int", nullable: false),
                    EmisorRegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    MotivoCancelacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCancelacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TipoComprobanteId = table.Column<int>(type: "int", nullable: false),
                    EmisorRfc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRazonSocial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorDomicilioFiscal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    Folio = table.Column<int>(type: "int", nullable: true),
                    FileXmlTimbrado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoCertificadoSat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facturas", x => x.Id);
                    table.ForeignKey(
                        name: "fk_factura_copmany",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_factura_emisorregimenfiscal",
                        column: x => x.EmisorRegimenFiscalId,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_factura_formapago",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_factura_medotodopago",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodoPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_factura_tipocomprobante",
                        column: x => x.TipoComprobanteId,
                        principalTable: "TipoComprobantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_factura_tipomoneda",
                        column: x => x.TipoMonedaId,
                        principalTable: "TipoMonedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_factura_usocfdi",
                        column: x => x.UsoCfdiId,
                        principalTable: "UsoCfdis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_CompanyId",
                table: "Facturas",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_EmisorRegimenFiscalId",
                table: "Facturas",
                column: "EmisorRegimenFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_FormaPagoId",
                table: "Facturas",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_MetodoPagoId",
                table: "Facturas",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_TipoComprobanteId",
                table: "Facturas",
                column: "TipoComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_TipoMonedaId",
                table: "Facturas",
                column: "TipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_UsoCfdiId",
                table: "Facturas",
                column: "UsoCfdiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facturas");
        }
    }
}
