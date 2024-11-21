using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoTipoEstatusReembolso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Rembolsos");

            migrationBuilder.AddColumn<int>(
                name: "EstatusId",
                table: "Rembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoEstatusReembolso",
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
                    table.PrimaryKey("PK_TipoEstatusReembolso", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rembolsos_EstatusId",
                table: "Rembolsos",
                column: "EstatusId");

            migrationBuilder.AddForeignKey(
                name: "fk_id_estatus_reembolso",
                table: "Rembolsos",
                column: "EstatusId",
                principalTable: "TipoEstatusReembolso",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_estatus_reembolso",
                table: "Rembolsos");

            migrationBuilder.DropTable(
                name: "TipoEstatusReembolso");

            migrationBuilder.DropIndex(
                name: "IX_Rembolsos_EstatusId",
                table: "Rembolsos");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "Rembolsos");

            migrationBuilder.AddColumn<short>(
                name: "Estatus",
                table: "Rembolsos",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
