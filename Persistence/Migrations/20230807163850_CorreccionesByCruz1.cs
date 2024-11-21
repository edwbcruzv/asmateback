using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionesByCruz1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            //migrationBuilder.DropTable(
            //    name: "TipoImpuesto");

            //migrationBuilder.DropIndex(
            //    name: "IX_MovimientoReembolsos_TipoImpuestoId",
            //    table: "MovimientoReembolsos");

            //migrationBuilder.DropColumn(
            //    name: "TipoImpuestoId",
            //    table: "MovimientoReembolsos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoImpuestoId",
                table: "MovimientoReembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoImpuesto",
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
                    table.PrimaryKey("PK_TipoImpuesto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_TipoImpuestoId",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_tipoimpuesto_movreembolso",
                table: "MovimientoReembolsos",
                column: "TipoImpuestoId",
                principalTable: "TipoImpuesto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
