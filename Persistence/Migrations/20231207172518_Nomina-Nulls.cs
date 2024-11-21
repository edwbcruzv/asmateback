using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NominaNulls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nominas_TipoDeducciones_TipoDeduccionId",
                table: "Nominas");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominas_TipoPercepciones_TipoPercepcionId",
                table: "Nominas");

            migrationBuilder.AlterColumn<int>(
                name: "TipoPercepcionId",
                table: "Nominas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TipoDeduccionId",
                table: "Nominas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_TipoDeducciones_TipoDeduccionId",
                table: "Nominas",
                column: "TipoDeduccionId",
                principalTable: "TipoDeducciones",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_TipoPercepciones_TipoPercepcionId",
                table: "Nominas",
                column: "TipoPercepcionId",
                principalTable: "TipoPercepciones",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nominas_TipoDeducciones_TipoDeduccionId",
                table: "Nominas");

            migrationBuilder.DropForeignKey(
                name: "FK_Nominas_TipoPercepciones_TipoPercepcionId",
                table: "Nominas");

            migrationBuilder.AlterColumn<int>(
                name: "TipoPercepcionId",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TipoDeduccionId",
                table: "Nominas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_TipoDeducciones_TipoDeduccionId",
                table: "Nominas",
                column: "TipoDeduccionId",
                principalTable: "TipoDeducciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nominas_TipoPercepciones_TipoPercepcionId",
                table: "Nominas",
                column: "TipoPercepcionId",
                principalTable: "TipoPercepciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
