using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addEstadoInViatico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstadoId",
                table: "Viaticos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Viaticos_EstadoId",
                table: "Viaticos",
                column: "EstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Viaticos_Estados_EstadoId",
                table: "Viaticos",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Viaticos_Estados_EstadoId",
                table: "Viaticos");

            migrationBuilder.DropIndex(
                name: "IX_Viaticos_EstadoId",
                table: "Viaticos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Viaticos");
        }
    }
}
