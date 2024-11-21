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
    internal class SistemaDepartamentoConfig : IEntityTypeConfiguration<SistemaDepartamento>
    {
        public void Configure(EntityTypeBuilder<SistemaDepartamento> builder)
        {
            builder.HasKey(sd => new { sd.SistemaId, sd.DepartamentoId });

            builder.HasOne(sd => sd.Sistema)
                .WithMany(s => s.SistemaDepartamento)
                .HasForeignKey(sd => sd.SistemaId);

            builder.HasOne(sd => sd.Departamento)
                .WithMany(s => s.SistemaDepartamento)
                .HasForeignKey(sd => sd.DepartamentoId);
        }
    }
}
