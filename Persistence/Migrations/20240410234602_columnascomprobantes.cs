using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class columnascomprobantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PathPDF",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMovimiento",
                table: "Comprobantes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "AnoyMes",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Descuento",
                table: "Comprobantes",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmisorNombre",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmisorRFC",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaTimbrado",
                table: "Comprobantes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Folio",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FormaPagoId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "IEPS",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ISH",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ISR",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "IVARetenidos",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "IVATrasladados",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineaCaptura",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LugarExpedicion",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MetodoPagoId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptorNombre",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceptorRFC",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegimenFiscalId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SubTotal",
                table: "Comprobantes",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "TipoCambio",
                table: "Comprobantes",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoComprobanteId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoComprobantes",
                table: "Comprobantes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoImpuestoId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TipoMonedaId",
                table: "Comprobantes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_FormaPagoId",
                table: "Comprobantes",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_MetodoPagoId",
                table: "Comprobantes",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_TipoComprobanteId",
                table: "Comprobantes",
                column: "TipoComprobanteId");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_TipoImpuestoId",
                table: "Comprobantes",
                column: "TipoImpuestoId");

            migrationBuilder.CreateIndex(
                name: "IX_Comprobantes_TipoMonedaId",
                table: "Comprobantes",
                column: "TipoMonedaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_FormaPagos_FormaPagoId",
                table: "Comprobantes",
                column: "FormaPagoId",
                principalTable: "FormaPagos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_MetodoPagos_MetodoPagoId",
                table: "Comprobantes",
                column: "MetodoPagoId",
                principalTable: "MetodoPagos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_TipoComprobantes_TipoComprobanteId",
                table: "Comprobantes",
                column: "TipoComprobanteId",
                principalTable: "TipoComprobantes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_TipoImpuestos_TipoImpuestoId",
                table: "Comprobantes",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuestos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comprobantes_TipoMonedas_TipoMonedaId",
                table: "Comprobantes",
                column: "TipoMonedaId",
                principalTable: "TipoMonedas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_FormaPagos_FormaPagoId",
                table: "Comprobantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_MetodoPagos_MetodoPagoId",
                table: "Comprobantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_TipoComprobantes_TipoComprobanteId",
                table: "Comprobantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_TipoImpuestos_TipoImpuestoId",
                table: "Comprobantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Comprobantes_TipoMonedas_TipoMonedaId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_FormaPagoId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_MetodoPagoId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_TipoComprobanteId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_TipoImpuestoId",
                table: "Comprobantes");

            migrationBuilder.DropIndex(
                name: "IX_Comprobantes_TipoMonedaId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "AnoyMes",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Descuento",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "EmisorNombre",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "EmisorRFC",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "FechaTimbrado",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Folio",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "FormaPagoId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "IEPS",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "ISH",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "ISR",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "IVARetenidos",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "IVATrasladados",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "LineaCaptura",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "LugarExpedicion",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "MetodoPagoId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "ReceptorNombre",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "ReceptorRFC",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "RegimenFiscalId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "TipoCambio",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "TipoComprobanteId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "TipoComprobantes",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "TipoImpuestoId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "TipoMonedaId",
                table: "Comprobantes");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Comprobantes");

            migrationBuilder.AlterColumn<string>(
                name: "PathPDF",
                table: "Comprobantes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaMovimiento",
                table: "Comprobantes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
