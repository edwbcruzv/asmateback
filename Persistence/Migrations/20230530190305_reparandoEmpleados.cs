using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class reparandoEmpleados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Banco_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_Puesto_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_RegimenFiscal_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoContrato_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoIncapacidad_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoRegimen_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoRiesgoTrabajo_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_user_employee",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoRiesgoTrabajoId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoRegimenId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoIncapacidadId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoContratoId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RegimenFiscalId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PuestoId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_Banco_employee",
                table: "Employees",
                column: "BancoId",
                principalTable: "Bancos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_Puesto_employee",
                table: "Employees",
                column: "PuestoId",
                principalTable: "Puestos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_RegimenFiscal_employee",
                table: "Employees",
                column: "RegimenFiscalId",
                principalTable: "RegimenFiscal",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoContrato_employee",
                table: "Employees",
                column: "TipoContratoId",
                principalTable: "TipoContratos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoIncapacidad_employee",
                table: "Employees",
                column: "TipoIncapacidadId",
                principalTable: "TipoIncapacidads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoRegimen_employee",
                table: "Employees",
                column: "TipoRegimenId",
                principalTable: "TipoRegimens",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_TipoRiesgoTrabajo_employee",
                table: "Employees",
                column: "TipoRiesgoTrabajoId",
                principalTable: "TipoRiesgoTrabajos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "fk_user_employee",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_Banco_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_Puesto_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_RegimenFiscal_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoContrato_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoIncapacidad_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoRegimen_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_TipoRiesgoTrabajo_employee",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "fk_user_employee",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoRiesgoTrabajoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoRegimenId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoIncapacidadId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoContratoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RegimenFiscalId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PuestoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BancoId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_Banco_employee",
                table: "Employees",
                column: "BancoId",
                principalTable: "Bancos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_Puesto_employee",
                table: "Employees",
                column: "PuestoId",
                principalTable: "Puestos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_RegimenFiscal_employee",
                table: "Employees",
                column: "RegimenFiscalId",
                principalTable: "RegimenFiscal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_TipoContrato_employee",
                table: "Employees",
                column: "TipoContratoId",
                principalTable: "TipoContratos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_TipoIncapacidad_employee",
                table: "Employees",
                column: "TipoIncapacidadId",
                principalTable: "TipoIncapacidads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_TipoRegimen_employee",
                table: "Employees",
                column: "TipoRegimenId",
                principalTable: "TipoRegimens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_TipoRiesgoTrabajo_employee",
                table: "Employees",
                column: "TipoRiesgoTrabajoId",
                principalTable: "TipoRiesgoTrabajos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_employee",
                table: "Employees",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
