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
    public class RegistroAsistenciaConfig : IEntityTypeConfiguration<RegistroAsistencia>
    {
        public void Configure(EntityTypeBuilder<RegistroAsistencia> builder)
        {
            builder.HasOne(d => d.Employee)
                .WithMany(p => p.RegistroAsistencias)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("fk_employeeid_registroasistencias")
                .IsRequired();

            builder.HasOne(d => d.TipoAsistencia)
                .WithMany(p => p.RegistroAsistencias)
                .HasForeignKey(d => d.TipoAsistenciaId)
                .HasConstraintName("fk_employeeid_TipoAsistencia")
                .IsRequired();


        }
    }
}

