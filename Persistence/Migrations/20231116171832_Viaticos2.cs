using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Viaticos2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Estatus",
            //    table: "Incidencias");

            //migrationBuilder.DropColumn(
            //    name: "Tipo",
            //    table: "Incidencias");

            //migrationBuilder.AddColumn<int>(
            //    name: "EstatusId",
            //    table: "Incidencias",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "TipoId",
            //    table: "Incidencias",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Viaticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BancoId = table.Column<int>(type: "int", nullable: false),
                    NoCuenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SrcPDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeePagoId = table.Column<int>(type: "int", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoRecibido = table.Column<float>(type: "real", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viaticos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Viaticos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Viaticos_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Viaticos_Employees_EmployeePagoId",
                        column: x => x.EmployeePagoId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Viaticos_CompanyId",
                table: "Viaticos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Viaticos_EmployeeId",
                table: "Viaticos",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Viaticos_EmployeePagoId",
                table: "Viaticos",
                column: "EmployeePagoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Viaticos");

            //migrationBuilder.DropColumn(
            //    name: "EstatusId",
            //    table: "Incidencias");

            //migrationBuilder.DropColumn(
            //    name: "TipoId",
            //    table: "Incidencias");

            //migrationBuilder.AddColumn<string>(
            //    name: "Estatus",
            //    table: "Incidencias",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "Tipo",
            //    table: "Incidencias",
            //    type: "nvarchar(max)",
            //    nullable: false,
            //    defaultValue: "");
        }
    }
}
