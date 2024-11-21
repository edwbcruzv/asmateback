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
    public class EmployeesConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {


            builder.HasOne(d => d.Company)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_Company_employee")
                .IsRequired();

            builder.HasOne(d => d.User)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_employee")
                .IsRequired(false);

            builder.HasOne(d => d.Banco)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.BancoId)
                .HasConstraintName("fk_Banco_employee")
                .IsRequired(false);


            builder.HasOne(d => d.Puesto)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.PuestoId)
                .HasConstraintName("fk_Puesto_employee")
                .IsRequired(false);


            builder.HasOne(d => d.TipoContrato)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoContratoId)
                .HasConstraintName("fk_TipoContrato_employee")
                .IsRequired(false);

            builder.HasOne(d => d.TipoJornada)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoJornadaId)
                .HasConstraintName("fk_TipoJornada_employee")
                .IsRequired(false);

            builder.HasOne(d => d.TipoRiesgoTrabajo)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoRiesgoTrabajoId)
                .HasConstraintName("fk_TipoRiesgoTrabajo_employee")
                .IsRequired(false);

            builder.HasOne(d => d.TipoContrato)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoContratoId)
                .HasConstraintName("fk_TipoContrato_employee")
                .IsRequired(false);


            builder.HasOne(d => d.TipoRegimen)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoRegimenId)
                .HasConstraintName("fk_TipoRegimen_employee")
                .IsRequired(false);


            builder.HasOne(d => d.TipoRiesgoTrabajo)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoRiesgoTrabajoId)
                .HasConstraintName("fk_TipoRiesgoTrabajo_employee")
                .IsRequired(false);


            builder.HasOne(d => d.TipoIncapacidad)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.TipoIncapacidadId)
                .HasConstraintName("fk_TipoIncapacidad_employee")
                .IsRequired(false);


            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.RegimenFiscalId)
                .HasConstraintName("fk_RegimenFiscal_employee")
                .IsRequired(false);

            // Agregando el registro del jefe del empleado

            builder.HasOne(p => p.Jefe)
                .WithMany(p => p.Dependientes)
                .HasForeignKey(p => p.JefeId)
                .IsRequired(false);
        }
    }
}
