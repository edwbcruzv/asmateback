using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoReembolsoCorreccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos");

            migrationBuilder.DropIndex(
                name: "IX_Rembolsos_UserId",
                table: "Rembolsos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Rembolsos");

            migrationBuilder.RenameColumn(
                name: "UsuarioIdPagado",
                table: "Rembolsos",
                newName: "UsuarioIdPago");

            migrationBuilder.AlterColumn<string>(
                name: "SrcPdfPagoComprobante",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SrcPdfFichaPago",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Clabe",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<double>(
                name: "IVA",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "ISR",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "ISH",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "IEPS",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "Concepto",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RegimenFiscalId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Uuid",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Rembolsos_UsuarioIdPago",
                table: "Rembolsos",
                column: "UsuarioIdPago");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_RegimenFiscalId",
                table: "MovimientoReembolsos",
                column: "RegimenFiscalId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_regimenfiscal_movreembolso",
                table: "MovimientoReembolsos",
                column: "RegimenFiscalId",
                principalTable: "RegimenFiscal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos",
                column: "UsuarioIdPago",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_regimenfiscal_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.DropForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos");

            migrationBuilder.DropIndex(
                name: "IX_Rembolsos_UsuarioIdPago",
                table: "Rembolsos");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoReembolsos_RegimenFiscalId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "Concepto",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "RegimenFiscalId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "MovimientoReembolsos");

            migrationBuilder.RenameColumn(
                name: "UsuarioIdPago",
                table: "Rembolsos",
                newName: "UsuarioIdPagado");

            migrationBuilder.AlterColumn<string>(
                name: "SrcPdfPagoComprobante",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SrcPdfFichaPago",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Clabe",
                table: "Rembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Rembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "IVA",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ISR",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "ISH",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "IEPS",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rembolsos_UserId",
                table: "Rembolsos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
