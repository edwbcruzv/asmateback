using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MovReembolso_Src : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SrcFile",
                table: "MovimientoReembolsos",
                newName: "XMLSrcFile");

            migrationBuilder.AddColumn<string>(
                name: "PDFSrcFile",
                table: "MovimientoReembolsos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PDFSrcFile",
                table: "MovimientoReembolsos");

            migrationBuilder.RenameColumn(
                name: "XMLSrcFile",
                table: "MovimientoReembolsos",
                newName: "SrcFile");
        }
    }
}
