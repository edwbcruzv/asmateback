using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjusteTicketYOtrosMas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_EmployeeId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Tickets",
                newName: "EmployeeCreadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_EmployeeId",
                table: "Tickets",
                newName: "IX_Tickets_EmployeeCreadorId");

            migrationBuilder.AlterColumn<string>(
                name: "OpcionSubMenu",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAsignadoId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SistemaDepartamento",
                columns: table => new
                {
                    SistemaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SistemaDepartamento", x => new { x.SistemaId, x.DepartamentoId });
                    table.ForeignKey(
                        name: "FK_SistemaDepartamento_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SistemaDepartamento_Sistemas_SistemaId",
                        column: x => x.SistemaId,
                        principalTable: "Sistemas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_EmployeeAsignadoId",
                table: "Tickets",
                column: "EmployeeAsignadoId");

            migrationBuilder.CreateIndex(
                name: "IX_SistemaDepartamento_DepartamentoId",
                table: "SistemaDepartamento",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_EmployeeAsignadoId",
                table: "Tickets",
                column: "EmployeeAsignadoId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_EmployeeCreadorId",
                table: "Tickets",
                column: "EmployeeCreadorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_EmployeeAsignadoId",
                table: "Tickets");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Employees_EmployeeCreadorId",
                table: "Tickets");

            migrationBuilder.DropTable(
                name: "SistemaDepartamento");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_EmployeeAsignadoId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "EmployeeAsignadoId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "EmployeeCreadorId",
                table: "Tickets",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_EmployeeCreadorId",
                table: "Tickets",
                newName: "IX_Tickets_EmployeeId");

            migrationBuilder.AlterColumn<string>(
                name: "OpcionSubMenu",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Employees_EmployeeId",
                table: "Tickets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
