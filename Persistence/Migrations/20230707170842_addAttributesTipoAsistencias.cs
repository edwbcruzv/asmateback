using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addAttributesTipoAsistencias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SePagaAguinaldo",
                table: "TipoAsistencias",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SePagaNomina",
                table: "TipoAsistencias",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SePagaPtu",
                table: "TipoAsistencias",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SePagaAguinaldo",
                table: "TipoAsistencias");

            migrationBuilder.DropColumn(
                name: "SePagaNomina",
                table: "TipoAsistencias");

            migrationBuilder.DropColumn(
                name: "SePagaPtu",
                table: "TipoAsistencias");
        }
    }
}
