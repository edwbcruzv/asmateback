using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class repanrandoPuestoDepar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Puestos_Departamentos_DepartamentoId1",
                table: "Puestos");

            migrationBuilder.DropIndex(
                name: "IX_Puestos_DepartamentoId1",
                table: "Puestos");

            migrationBuilder.DropColumn(
                name: "DepartamentoId1",
                table: "Puestos");

            migrationBuilder.AlterColumn<int>(
                name: "DepartamentoId",
                table: "Puestos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Departamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Puestos_DepartamentoId",
                table: "Puestos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_CompanyId",
                table: "Departamentos",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "fk_CompanyId_Departamentos",
                table: "Departamentos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_DepartamentoId_Puestos",
                table: "Puestos",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_CompanyId_Departamentos",
                table: "Departamentos");

            migrationBuilder.DropForeignKey(
                name: "fk_DepartamentoId_Puestos",
                table: "Puestos");

            migrationBuilder.DropIndex(
                name: "IX_Puestos_DepartamentoId",
                table: "Puestos");

            migrationBuilder.DropIndex(
                name: "IX_Departamentos_CompanyId",
                table: "Departamentos");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Departamentos");

            migrationBuilder.AlterColumn<string>(
                name: "DepartamentoId",
                table: "Puestos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId1",
                table: "Puestos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Puestos_DepartamentoId1",
                table: "Puestos",
                column: "DepartamentoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Puestos_Departamentos_DepartamentoId1",
                table: "Puestos",
                column: "DepartamentoId1",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
