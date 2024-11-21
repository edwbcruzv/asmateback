using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Migrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RegimenFiscal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegimenFiscalCve = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    RegimenFiscalDesc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegimenFiscal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUser = table.Column<int>(type: "int", nullable: false),
                    SalaryDays = table.Column<int>(type: "int", nullable: false),
                    CompanyStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompanyProfile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistroPatronal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    RepresentanteLegal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rfc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RazonSocial = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Certificado = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PrivateKeyFile = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    PassPrivateKey = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    FolioFactura = table.Column<int>(type: "int", nullable: true),
                    Calle = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NoExt = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NoInt = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Colonia = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Municipio = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Estado = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Pais = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                    table.ForeignKey(
                        name: "fk_regimenFiscal_companies",
                        column: x => x.RegimenFiscalId,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    Rfc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    RazonSocial = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Calle = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NoExt = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    NoInt = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Colonia = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Municipio = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Estado = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Pais = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CodigoPostal = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Estatus = table.Column<bool>(type: "bit", nullable: true),
                    TipoPersona = table.Column<short>(type: "smallint", nullable: true),
                    RegimenFiscalId = table.Column<int>(type: "int", nullable: false),
                    Clabe = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Correos = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "fk_id_company",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "fk_regimenFiscal_cliente",
                        column: x => x.RegimenFiscalId,
                        principalTable: "RegimenFiscal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyId",
                table: "Clients",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_RegimenFiscalId",
                table: "Clients",
                column: "RegimenFiscalId");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_RegimenFiscalId",
                table: "Companies",
                column: "RegimenFiscalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "RegimenFiscal");
        }
    }
}
