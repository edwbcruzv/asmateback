using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class tableFacturaMovimientos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacturaMovimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaId = table.Column<int>(type: "int", nullable: false),
                    CveProductoId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Descuento = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Iva = table.Column<bool>(type: "bit", nullable: false),
                    Iva6 = table.Column<bool>(type: "bit", nullable: false),
                    RetencionIva = table.Column<bool>(type: "bit", nullable: false),
                    RetencionIsr = table.Column<bool>(type: "bit", nullable: false),
                    ObjetoImpuestoId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacturaMovimientos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_facturamovimiento_CveProducto",
                        column: x => x.CveProductoId,
                        principalTable: "CveProductos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_facturamovimiento_Factura",
                        column: x => x.FacturaId,
                        principalTable: "Facturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_facturamovimiento_ObjetoImpuesto",
                        column: x => x.ObjetoImpuestoId,
                        principalTable: "ObjetoImpuestos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_facturamovimiento_UnidadMedida",
                        column: x => x.UnidadMedidaId,
                        principalTable: "UnidadMedidas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacturaMovimientos_CveProductoId",
                table: "FacturaMovimientos",
                column: "CveProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaMovimientos_FacturaId",
                table: "FacturaMovimientos",
                column: "FacturaId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaMovimientos_ObjetoImpuestoId",
                table: "FacturaMovimientos",
                column: "ObjetoImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturaMovimientos_UnidadMedidaId",
                table: "FacturaMovimientos",
                column: "UnidadMedidaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturaMovimientos");
        }
    }
}
