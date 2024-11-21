using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreandoTablaImpuestoByCruz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoImpuestoId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoImpuestos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoImpuestos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_TipoImpuestoId",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoReembolsos_TipoImpuestos_TipoImpuestoId",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuestos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientoReembolsos_TipoImpuestos_TipoImpuestoId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropTable(
                name: "TipoImpuestos");

            migrationBuilder.DropIndex(
                name: "IX_MovimientoReembolsos_TipoImpuestoId",
                table: "MovimientoReembolsos");

            migrationBuilder.DropColumn(
                name: "TipoImpuestoId",
                table: "MovimientoReembolsos");
        }
    }
}
