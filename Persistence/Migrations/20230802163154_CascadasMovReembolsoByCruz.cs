using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CascadasMovReembolsoByCruz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_formapago_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_metodopago_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tiporeembolso_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.AddForeignKey(
                name: "fk_id_formapago_movreembolso",
                table: "MovimientoReembolsos",
                column: "FormaPagoId",
                principalTable: "FormaPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_id_metodopago_movreembolso",
                table: "MovimientoReembolsos",
                column: "MetodoPagoId",
                principalTable: "MetodoPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoComprobanteId",
                principalTable: "TipoComprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoMonedaId",
                principalTable: "TipoMonedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tiporeembolso_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoReembolsoId",
                principalTable: "TipoReembolso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_formapago_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_metodopago_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tiporeembolso_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.AddForeignKey(
                name: "fk_id_formapago_movreembolso",
                table: "MovimientoReembolsos",
                column: "FormaPagoId",
                principalTable: "FormaPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_id_metodopago_movreembolso",
                table: "MovimientoReembolsos",
                column: "MetodoPagoId",
                principalTable: "MetodoPagos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoComprobanteId",
                principalTable: "TipoComprobantes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoMonedaId",
                principalTable: "TipoMonedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tiporeembolso_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoReembolsoId",
                principalTable: "TipoReembolso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
