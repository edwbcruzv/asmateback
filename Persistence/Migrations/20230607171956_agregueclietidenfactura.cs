using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class agregueclietidenfactura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Facturas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Facturas_ClientId",
                table: "Facturas",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "fk_factura_client",
                table: "Facturas",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_factura_client",
                table: "Facturas");

            migrationBuilder.DropIndex(
                name: "IX_Facturas_ClientId",
                table: "Facturas");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Facturas");
        }
    }
}
