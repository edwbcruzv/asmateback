using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correccionPagoComplemento4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "UsoCfdiId",
                table: "ComplementoPagos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_UsoCfdiId",
                table: "ComplementoPagos",
                column: "UsoCfdiId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplementoPagos_UsoCfdis_UsoCfdiId",
                table: "ComplementoPagos",
                column: "UsoCfdiId",
                principalTable: "UsoCfdis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplementoPagos_UsoCfdis_UsoCfdiId",
                table: "ComplementoPagos");

            migrationBuilder.DropIndex(
                name: "IX_ComplementoPagos_UsoCfdiId",
                table: "ComplementoPagos");

            migrationBuilder.DropColumn(
                name: "UsoCfdiId",
                table: "ComplementoPagos");

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
    }
}
