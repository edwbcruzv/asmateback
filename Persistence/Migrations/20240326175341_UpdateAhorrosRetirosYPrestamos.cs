using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAhorrosRetirosYPrestamos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SrcDocSolicitudRetiro",
                table: "Prestamos",
                newName: "SrcDocContanciaTransferencia");

            migrationBuilder.RenameColumn(
                name: "FechaEstatusInactivo",
                table: "Prestamos",
                newName: "FechaEstatusRechazado");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusAutorizado",
                table: "RetirosAhorroVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusPendiente",
                table: "RetirosAhorroVoluntario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusRechazado",
                table: "RetirosAhorroVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaTransferencia",
                table: "RetirosAhorroVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrcDocContanciaTransferencia",
                table: "RetirosAhorroVoluntario",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusActivo",
                table: "AhorrosVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusFiniquitado",
                table: "AhorrosVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusPendiente",
                table: "AhorrosVoluntario",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaEstatusRechazado",
                table: "AhorrosVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaTransferencia",
                table: "AhorrosVoluntario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SrcDocContanciaTransferencia",
                table: "AhorrosVoluntario",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaEstatusAutorizado",
                table: "RetirosAhorroVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusPendiente",
                table: "RetirosAhorroVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusRechazado",
                table: "RetirosAhorroVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaTransferencia",
                table: "RetirosAhorroVoluntario");

            migrationBuilder.DropColumn(
                name: "SrcDocContanciaTransferencia",
                table: "RetirosAhorroVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusActivo",
                table: "AhorrosVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusFiniquitado",
                table: "AhorrosVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusPendiente",
                table: "AhorrosVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaEstatusRechazado",
                table: "AhorrosVoluntario");

            migrationBuilder.DropColumn(
                name: "FechaTransferencia",
                table: "AhorrosVoluntario");

            migrationBuilder.DropColumn(
                name: "SrcDocContanciaTransferencia",
                table: "AhorrosVoluntario");

            migrationBuilder.RenameColumn(
                name: "SrcDocContanciaTransferencia",
                table: "Prestamos",
                newName: "SrcDocSolicitudRetiro");

            migrationBuilder.RenameColumn(
                name: "FechaEstatusRechazado",
                table: "Prestamos",
                newName: "FechaEstatusInactivo");
        }
    }
}
