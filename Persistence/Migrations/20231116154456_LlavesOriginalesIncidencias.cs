using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class LlavesOriginalesIncidencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias",
                columns: new[] { "Id", "EmpleadoId", "CompanyId" });
        }
    }
}
