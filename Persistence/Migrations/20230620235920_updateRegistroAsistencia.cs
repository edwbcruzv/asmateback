using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateRegistroAsistencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistroAsistencias_TipoAsistencias_tipoAsistenciaId",
                table: "RegistroAsistencias");

            migrationBuilder.DropColumn(
                name: "TipoAsistencia",
                table: "RegistroAsistencias");

            migrationBuilder.RenameColumn(
                name: "tipoAsistenciaId",
                table: "RegistroAsistencias",
                newName: "TipoAsistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistroAsistencias_tipoAsistenciaId",
                table: "RegistroAsistencias",
                newName: "IX_RegistroAsistencias_TipoAsistenciaId");

            migrationBuilder.AddForeignKey(
                name: "fk_employeeid_TipoAsistencia",
                table: "RegistroAsistencias",
                column: "TipoAsistenciaId",
                principalTable: "TipoAsistencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_employeeid_TipoAsistencia",
                table: "RegistroAsistencias");

            migrationBuilder.RenameColumn(
                name: "TipoAsistenciaId",
                table: "RegistroAsistencias",
                newName: "tipoAsistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistroAsistencias_TipoAsistenciaId",
                table: "RegistroAsistencias",
                newName: "IX_RegistroAsistencias_tipoAsistenciaId");

            migrationBuilder.AddColumn<int>(
                name: "TipoAsistencia",
                table: "RegistroAsistencias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistroAsistencias_TipoAsistencias_tipoAsistenciaId",
                table: "RegistroAsistencias",
                column: "tipoAsistenciaId",
                principalTable: "TipoAsistencias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
