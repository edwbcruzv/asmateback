using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TablaImpuestosFKByCruz1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AlterColumn<int>(
                name: "TipoImpuestoId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipoimpuesto_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_tipoimpuesto_movreembolso",
                table: "MovimientoReembolsos");

            migrationBuilder.AlterColumn<int>(
                name: "TipoImpuestoId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoReembolsos_TipoImpuestos_TipoImpuestoId",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuestos",
                principalColumn: "Id");
        }
    }
}
