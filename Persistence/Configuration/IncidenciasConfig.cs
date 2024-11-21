using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class IncidenciasConfig : IEntityTypeConfiguration<Incidencia>
    {
        public void Configure(EntityTypeBuilder<Incidencia> builder)
        {
            builder.ToTable("Incidencias");

            /*builder.Property(x => x.Id)
                .UseIdentityColumn();*/
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Company)
                .WithMany(x => x.Incidencias)
                .HasForeignKey(x => x.CompanyId)
                .HasConstraintName("fk_company_incidencias")
                .IsRequired();

            builder.HasOne(x => x.Employee)
                .WithMany(y => y.Incidencias)
                .HasForeignKey(x => x.EmpleadoId)
                .HasConstraintName("fk_employee_incidencias")
                .IsRequired();

            //builder.HasKey(x => new { x.Id, x.EmpleadoId, x.CompanyId });

            builder.Property(x => x.FechaInicio)
                .IsRequired();

            builder.HasOne(x => x.TipoIncidencia)
                .WithMany(x => x.Incidencias)
                .IsRequired()
                .HasForeignKey(x => x.TipoId)
                .HasConstraintName("fk_TipoIncidencia_Incidencias");

            builder.HasOne(x => x.TipoEstatusIncidencia)
                .WithMany(x => x.Incidencias)
                .IsRequired()
                .HasForeignKey(x => x.EstatusId)
                .HasConstraintName("fk_TipoEstatusIncidencia_Incidencias");
        }
    }
}
