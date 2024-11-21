using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PruebaLlaveCompuestaIncidencia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "pk_company_incidencias",
                table: "Incidencias");

            migrationBuilder.DropForeignKey(
                name: "pk_employee_incidencias",
                table: "Incidencias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Incidencias",
                newName: "EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_Incidencias_EmployeeId",
                table: "Incidencias",
                newName: "IX_Incidencias_EmpleadoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias",
                columns: new[] { "Id", "EmpleadoId", "CompanyId" });

            migrationBuilder.AddForeignKey(
                name: "fk_company_incidencias",
                table: "Incidencias",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_employee_incidencias",
                table: "Incidencias",
                column: "EmpleadoId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_company_incidencias",
                table: "Incidencias");

            migrationBuilder.DropForeignKey(
                name: "fk_employee_incidencias",
                table: "Incidencias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "Incidencias",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Incidencias_EmpleadoId",
                table: "Incidencias",
                newName: "IX_Incidencias_EmployeeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Incidencias",
                table: "Incidencias",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "pk_company_incidencias",
                table: "Incidencias",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "pk_employee_incidencias",
                table: "Incidencias",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
