using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ComprobanteSinXML2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComprobantesSinXML",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViaticoId = table.Column<int>(type: "int", nullable: false),
                    FechaMovimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PathPDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Folio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorRFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmisorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorRFC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceptorNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoMonedaId = table.Column<int>(type: "int", nullable: false),
                    FormaPagoId = table.Column<int>(type: "int", nullable: false),
                    Descuento = table.Column<float>(type: "real", nullable: false),
                    SubTotal = table.Column<float>(type: "real", nullable: false),
                    Total = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComprobantesSinXML", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComprobantesSinXML_FormaPagos_FormaPagoId",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprobantesSinXML_TipoMonedas_TipoMonedaId",
                        column: x => x.TipoMonedaId,
                        principalTable: "TipoMonedas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComprobantesSinXML_Viaticos_ViaticoId",
                        column: x => x.ViaticoId,
                        principalTable: "Viaticos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesSinXML_FormaPagoId",
                table: "ComprobantesSinXML",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesSinXML_TipoMonedaId",
                table: "ComprobantesSinXML",
                column: "TipoMonedaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComprobantesSinXML_ViaticoId",
                table: "ComprobantesSinXML",
                column: "ViaticoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComprobantesSinXML");
        }
    }
}
