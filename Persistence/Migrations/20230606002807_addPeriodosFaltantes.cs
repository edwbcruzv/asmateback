using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addPeriodosFaltantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoPeriocidadPagoId",
                table: "Periodos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Periodos_TipoPeriocidadPagoId",
                table: "Periodos",
                column: "TipoPeriocidadPagoId");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoPeriocidadPagoId_Periodos",
                table: "Periodos",
                column: "TipoPeriocidadPagoId",
                principalTable: "TipoPeriocidadPago",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_TipoPeriocidadPagoId_Periodos",
                table: "Periodos");

            migrationBuilder.DropIndex(
                name: "IX_Periodos_TipoPeriocidadPagoId",
                table: "Periodos");

            migrationBuilder.DropColumn(
                name: "TipoPeriocidadPagoId",
                table: "Periodos");
        }
    }
}
