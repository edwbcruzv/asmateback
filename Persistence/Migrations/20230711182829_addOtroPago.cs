using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addOtroPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoOtroPagoId",
                table: "Nominas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TipoOtroPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoOtroPagos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Nominas_TipoOtroPagoId",
                table: "Nominas",
                column: "TipoOtroPagoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_TipoOtroPagos_TipoOtroPagoId",
                table: "Nominas",
                column: "TipoOtroPagoId",
                principalTable: "TipoOtroPagos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nominas_TipoOtroPagos_TipoOtroPagoId",
                table: "Nominas");

            migrationBuilder.DropTable(
                name: "TipoOtroPagos");

            migrationBuilder.DropIndex(
                name: "IX_Nominas_TipoOtroPagoId",
                table: "Nominas");

            migrationBuilder.DropColumn(
                name: "TipoOtroPagoId",
                table: "Nominas");
        }
    }
}
