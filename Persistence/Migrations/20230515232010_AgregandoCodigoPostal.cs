using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoCodigoPostal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "CodigoPostales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CondigoPostal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Asentamiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoAsentamiento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodigoPostales", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CodigoPostales");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubMenuUserSelectors",
                table: "SubMenuUserSelectors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubMenus",
                table: "SubMenus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuUserSelectors",
                table: "MenuUserSelectors");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "SubMenuUserSelectors",
                newName: "SubMenuUserSelector");

            migrationBuilder.RenameTable(
                name: "SubMenus",
                newName: "SubMenu");

            migrationBuilder.RenameTable(
                name: "MenuUserSelectors",
                newName: "MenuUserSelector");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenuUserSelectors_UserId",
                table: "SubMenuUserSelector",
                newName: "IX_SubMenuUserSelector_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenuUserSelectors_SubMenuId",
                table: "SubMenuUserSelector",
                newName: "IX_SubMenuUserSelector_SubMenuId");

            migrationBuilder.RenameIndex(
                name: "IX_SubMenus_MenuId",
                table: "SubMenu",
                newName: "IX_SubMenu_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuUserSelectors_UserId",
                table: "MenuUserSelector",
                newName: "IX_MenuUserSelector_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuUserSelectors_MenuId",
                table: "MenuUserSelector",
                newName: "IX_MenuUserSelector_MenuId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubMenuUserSelector",
                table: "SubMenuUserSelector",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubMenu",
                table: "SubMenu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuUserSelector",
                table: "MenuUserSelector",
                column: "Id");
        }
    }
}
