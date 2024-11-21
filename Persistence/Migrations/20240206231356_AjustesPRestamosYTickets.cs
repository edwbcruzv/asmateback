using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AjustesPRestamosYTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Prioridad",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PeriodoFinal",
                table: "Prestamos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaTransferencia",
                table: "Prestamos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinal",
                table: "Prestamos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusActivo",
                table: "Prestamos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusFiniquitado",
                table: "Prestamos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusInactivo",
                table: "Prestamos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusPendiente",
                table: "Prestamos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SrcDocAuto",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrcDocPago",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrcDocPrestamo",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrcDocRest",
                table: "Prestamos",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prioridad",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "FechaEstatusActivo",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "FechaEstatusFiniquitado",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "FechaEstatusInactivo",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "FechaEstatusPendiente",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "SrcDocAuto",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "SrcDocPago",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "SrcDocPrestamo",
                table: "Prestamos");

            migrationBuilder.DropColumn(
                name: "SrcDocRest",
                table: "Prestamos");

            migrationBuilder.AlterColumn<int>(
                name: "PeriodoFinal",
                table: "Prestamos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaTransferencia",
                table: "Prestamos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaFinal",
                table: "Prestamos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
