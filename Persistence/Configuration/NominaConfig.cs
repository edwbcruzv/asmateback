using Ardalis.Specification;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class NominaConfig : IEntityTypeConfiguration<Nomina>
    {
        public void Configure(EntityTypeBuilder<Nomina> builder)
        {
           

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_company_nominas")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_employee_nominas")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.EmisorRegimenFistalId)
                .HasConstraintName("fk_regimenFiscal_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.UsoCfdi)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.ReceptorUsoCfdiId)
                .HasConstraintName("fk_UsoCfdi_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.MetodoPago)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("fk_MetodoPago_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoPeriocidadPago)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.TipoPeriocidadPagoId)
                .HasConstraintName("fk_TipoPeriocidadPago_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Puesto)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.PuestoId)
                .HasConstraintName("fk_Puesto_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.TipoContrato)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.TipoContratoId)
                .HasConstraintName("fk_TipoContrato_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoJornada)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.TipoJornadaId)
                .HasConstraintName("fk_TipoJornada_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoRegimen)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.TipoRegimenId)
                .HasConstraintName("fk_TipoRegimen_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoRiesgoTrabajo)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.TipoRiesgoTrabajoId)
                .HasConstraintName("fk_TipoRiesgoTrabajo_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.Periodo)
                .WithMany(p => p.Nominas)
                .HasForeignKey(d => d.PeriodoId)
                .HasConstraintName("fk_Periodo_nominas")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);



        }
    }
}
