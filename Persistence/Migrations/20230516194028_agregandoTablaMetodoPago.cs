using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class agregandoTablaMetodoPago : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  

            migrationBuilder.CreateTable(
                name: "MetodoPagos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MetodoDePago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MetodoPagos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContractsUserCompanies",
                table: "ContractsUserCompanies");

            migrationBuilder.RenameTable(
                name: "ContractsUserCompanies",
                newName: "ContractsUserCompany");

            migrationBuilder.RenameIndex(
                name: "IX_ContractsUserCompanies_UserId",
                table: "ContractsUserCompany",
                newName: "IX_ContractsUserCompany_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContractsUserCompanies_CompanyId",
                table: "ContractsUserCompany",
                newName: "IX_ContractsUserCompany_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContractsUserCompany",
                table: "ContractsUserCompany",
                column: "Id");
        }
    }
}
