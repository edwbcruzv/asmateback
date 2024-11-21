using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class segundoversionComplementoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacturasAsociadas");

            migrationBuilder.CreateTable(
                name: "ComplementoPagoFacturas",
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
                    table.PrimaryKey("PK_ComplementoPagoFacturas", x => x.Id);
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
                name: "IX_ComplementoPagoFacturas_ComplementoPagoId",
                table: "ComplementoPagoFacturas",
                column: "ComplementoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagoFacturas_FacturaId",
                table: "ComplementoPagoFacturas",
                column: "FacturaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplementoPagoFacturas");

            migrationBuilder.CreateTable(
                name: "FacturasAsociadas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComplementoPagoId = table.Column<int>(type: "int", nullable: false),
                    FacturaId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Folio = table.Column<int>(type: "int", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monto = table.Column<double>(type: "float", nullable: false)
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
                name: "IX_FacturasAsociadas_ComplementoPagoId",
                table: "FacturasAsociadas",
                column: "ComplementoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_FacturasAsociadas_FacturaId",
                table: "FacturasAsociadas",
                column: "FacturaId");
        }
    }
}
