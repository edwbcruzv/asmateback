using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoMovReembolsoByCruz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "XMLSrcFile",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "LugarExpedicion",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaTimbrado",
                table: "MovimientoReembolsos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TipoMonedaId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_TipoMonedaId",
                table: "MovimientoReembolsos",
                column: "TipoMonedaId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoMonedaId",
                principalTable: "TipoMonedas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_tipomoneda_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoReembolsos_TipoMonedaId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "FechaTimbrado",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "TipoMonedaId",
                table: "MovimientoReembolsos");

            migrationBuilder.AlterColumn<string>(
                name: "XMLSrcFile",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LugarExpedicion",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
