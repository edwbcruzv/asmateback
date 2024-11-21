using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TipoIncidencias_TipoEstatusIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Incidencias");

            migrationBuilder.AddColumn<int>(
                name: "EstatusId",
                table: "Incidencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoId",
                table: "Incidencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TipoEstatusIncidencia",
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
                    table.PrimaryKey("PK_TipoEstatusIncidencia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoIncidencia",
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
                    table.PrimaryKey("PK_TipoIncidencia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_EstatusId",
                table: "Incidencias",
                column: "EstatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_TipoId",
                table: "Incidencias",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoEstatusIncidencia_Incidencias",
                table: "Incidencias",
                column: "EstatusId",
                principalTable: "TipoEstatusIncidencia",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoIncidencia_Incidencias",
                table: "Incidencias",
                column: "TipoId",
                principalTable: "TipoIncidencia",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_TipoEstatusIncidencia_Incidencias",
                table: "Incidencias");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoIncidencia_Incidencias",
                table: "Incidencias");

            migrationBuilder.DropTable(
                name: "TipoEstatusIncidencia");

            migrationBuilder.DropTable(
                name: "TipoIncidencia");

            migrationBuilder.DropIndex(
                name: "IX_Incidencias_EstatusId",
                table: "Incidencias");

            migrationBuilder.DropIndex(
                name: "IX_Incidencias_TipoId",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "EstatusId",
                table: "Incidencias");

            migrationBuilder.DropColumn(
                name: "TipoId",
                table: "Incidencias");

            migrationBuilder.AddColumn<string>(
                name: "Estatus",
                table: "Incidencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Incidencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
