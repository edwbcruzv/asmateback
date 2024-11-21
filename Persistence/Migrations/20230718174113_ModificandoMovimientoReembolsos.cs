using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoMovimientoReembolsos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_tipoarchivo_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropTable(
                name: "TipoArchivo");

            migrationBuilder.RenameColumn(
                name: "TipoArchivoId",
                table: "MovimientoReembolsos",
                newName: "TipoReembolsoId");

            migrationBuilder.RenameColumn(
                name: "RazonSocialReceptor",
                table: "MovimientoReembolsos",
                newName: "ReceptorRFC");

            migrationBuilder.RenameColumn(
                name: "Concepto",
                table: "MovimientoReembolsos",
                newName: "ReceptorNombre");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientoReembolsos_TipoArchivoId",
                table: "MovimientoReembolsos",
                newName: "IX_MovimientoReembolsos_TipoReembolsoId");

            migrationBuilder.AlterColumn<string>(
                name: "SrcFile",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmisorNombre",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmisorRFC",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoComprobanteId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoReembolso",
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
                    table.PrimaryKey("PK_TipoReembolso", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_TipoComprobanteId",
                table: "MovimientoReembolsos",
                column: "TipoComprobanteId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoComprobanteId",
                principalTable: "TipoComprobantes",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_tipocomprobante_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_tiporeembolso_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropTable(
                name: "TipoReembolso");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoReembolsos_TipoComprobanteId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "EmisorNombre",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "EmisorRFC",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "TipoComprobanteId",
                table: "MovimientoReembolsos");

            migrationBuilder.RenameColumn(
                name: "TipoReembolsoId",
                table: "MovimientoReembolsos",
                newName: "TipoArchivoId");

            migrationBuilder.RenameColumn(
                name: "ReceptorRFC",
                table: "MovimientoReembolsos",
                newName: "RazonSocialReceptor");

            migrationBuilder.RenameColumn(
                name: "ReceptorNombre",
                table: "MovimientoReembolsos",
                newName: "Concepto");

            migrationBuilder.RenameIndex(
                name: "IX_MovimientoReembolsos_TipoReembolsoId",
                table: "MovimientoReembolsos",
                newName: "IX_MovimientoReembolsos_TipoArchivoId");

            migrationBuilder.AlterColumn<double>(
                name: "SrcFile",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TipoArchivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArchivo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipoarchivo_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoArchivoId",
                principalTable: "TipoArchivo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
