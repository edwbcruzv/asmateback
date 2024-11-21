using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoReembolsos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rembolsos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Estatus = table.Column<short>(type: "smallint", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clabe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SrcPdfPagoComprobante = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioIdPagado = table.Column<int>(type: "int", nullable: false),
                    SrcPdfFichaPago = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Monto = table.Column<float>(type: "real", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rembolsos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_id_company_reembolso",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_id_user_reembolso",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TipoArchivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoArchivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientoReembolsos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISR = table.Column<double>(type: "float", nullable: false),
                    IVA = table.Column<double>(type: "float", nullable: false),
                    IEPS = table.Column<double>(type: "float", nullable: false),
                    ISH = table.Column<double>(type: "float", nullable: false),
                    TipoArchivoId = table.Column<int>(type: "int", nullable: false),
                    SrcFile = table.Column<double>(type: "float", nullable: false),
                    ReembolsoId = table.Column<int>(type: "int", nullable: false),
                    FormaPagoId = table.Column<int>(type: "int", nullable: false),
                    MetodoPagoId = table.Column<int>(type: "int", nullable: false),
                    LugarExpedicion = table.Column<int>(type: "int", nullable: false),
                    RazonSocialReceptor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subtotal = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientoReembolsos", x => x.Id);
                    table.ForeignKey(
                        name: "fk_id_formapago_movreembolso",
                        column: x => x.FormaPagoId,
                        principalTable: "FormaPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_id_metodopago_movreembolso",
                        column: x => x.MetodoPagoId,
                        principalTable: "MetodoPagos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_id_reembolso_movreembolso",
                        column: x => x.ReembolsoId,
                        principalTable: "Rembolsos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_id_tipoarchivo_movreembolso",
                        column: x => x.TipoArchivoId,
                        principalTable: "TipoArchivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_FormaPagoId",
                table: "MovimientoReembolsos",
                column: "FormaPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_MetodoPagoId",
                table: "MovimientoReembolsos",
                column: "MetodoPagoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_ReembolsoId",
                table: "MovimientoReembolsos",
                column: "ReembolsoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientoReembolsos_TipoArchivoId",
                table: "MovimientoReembolsos",
                column: "TipoArchivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rembolsos_CompanyId",
                table: "Rembolsos",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Rembolsos_UserId",
                table: "Rembolsos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientoReembolsos");

            migrationBuilder.DropTable(
                name: "Rembolsos");

            migrationBuilder.DropTable(
                name: "TipoArchivo");
        }
    }
}
