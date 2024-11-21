using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class correccionCompleentoApgo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
