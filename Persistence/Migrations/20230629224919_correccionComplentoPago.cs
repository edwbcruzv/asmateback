using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correccionComplentoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ComplementoPagos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComplementoPagos_ClientId",
                table: "ComplementoPagos",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "fk_complementopago_client",
                table: "ComplementoPagos",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_complementopago_client",
                table: "ComplementoPagos");

            migrationBuilder.DropForeignKey(
                name: "fk_employeeid_TipoAsistencia",
                table: "RegistroAsistencias");

            migrationBuilder.DropIndex(
                name: "IX_ComplementoPagos_ClientId",
                table: "ComplementoPagos");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ComplementoPagos");

            
        }
    }
}
