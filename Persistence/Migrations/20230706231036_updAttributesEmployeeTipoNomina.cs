using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updAttributesEmployeeTipoNomina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CuentaIndividual",
                table: "Employees",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FondoAhorroEmpleado",
                table: "Employees",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "FondoAhorroEmpresa",
                table: "Employees",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CuentaIndividual",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FondoAhorroEmpleado",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FondoAhorroEmpresa",
                table: "Employees");
        }
    }
}
