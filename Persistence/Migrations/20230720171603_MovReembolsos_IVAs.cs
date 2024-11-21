using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MovReembolsos_IVAs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IVA",
                table: "MovimientoReembolsos",
                newName: "IVATrasladados");

            migrationBuilder.AddColumn<double>(
                name: "IVARetenidos",
                table: "MovimientoReembolsos",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IVARetenidos",
                table: "MovimientoReembolsos");

            migrationBuilder.RenameColumn(
                name: "IVATrasladados",
                table: "MovimientoReembolsos",
                newName: "IVA");
        }
    }
}
