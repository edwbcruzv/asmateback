using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoReembolso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdPago",
                table: "Rembolsos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos",
                column: "UsuarioIdPago",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioIdPago",
                table: "Rembolsos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_id_user_reembolso",
                table: "Rembolsos",
                column: "UsuarioIdPago",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
