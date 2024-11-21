using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ActualizacionPrestamoFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SrcDocRest",
                table: "Prestamos",
                newName: "SrcDocSolicitudRetiro");

            migrationBuilder.RenameColumn(
                name: "SrcDocPrestamo",
                table: "Prestamos",
                newName: "SrcDocPagare");

            migrationBuilder.RenameColumn(
                name: "SrcDocPago",
                table: "Prestamos",
                newName: "SrcDocConstanciaRetiro");

            migrationBuilder.RenameColumn(
                name: "SrcDocAuto",
                table: "Prestamos",
                newName: "SrcDocAcuseFirmado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SrcDocSolicitudRetiro",
                table: "Prestamos",
                newName: "SrcDocRest");

            migrationBuilder.RenameColumn(
                name: "SrcDocPagare",
                table: "Prestamos",
                newName: "SrcDocPrestamo");

            migrationBuilder.RenameColumn(
                name: "SrcDocConstanciaRetiro",
                table: "Prestamos",
                newName: "SrcDocPago");

            migrationBuilder.RenameColumn(
                name: "SrcDocAcuseFirmado",
                table: "Prestamos",
                newName: "SrcDocAuto");
        }
    }
}
