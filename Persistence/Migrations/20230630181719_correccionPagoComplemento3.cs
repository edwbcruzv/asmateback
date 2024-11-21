using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correccionPagoComplemento3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceptorUsoCfdiId",
                table: "ComplementoPagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_ReceptorUsoCfdiId",
                table: "ComplementoPagos",
                column: "ReceptorUsoCfdiId");

            migrationBuilder.AddForeignKey(
                name: "fk_complementopago_uspcfdi",
                table: "ComplementoPagos",
                column: "ReceptorUsoCfdiId",
                principalTable: "UsoCfdis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_complementopago_uspcfdi",
                table: "ComplementoPagos");

            migrationBuilder.DropIndex(
                name: "IX_ComplementoPagos_ReceptorUsoCfdiId",
                table: "ComplementoPagos");

            migrationBuilder.DropColumn(
                name: "ReceptorUsoCfdiId",
                table: "ComplementoPagos");
        }
    }
}
