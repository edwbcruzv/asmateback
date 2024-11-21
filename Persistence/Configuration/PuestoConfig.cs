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
    public class PuestoConfig : IEntityTypeConfiguration<Puesto>
    {
        public void Configure(EntityTypeBuilder<Puesto> builder)
        {

            builder.HasOne(d => d.Departamento)
                .WithMany(p => p.Puestos)
                .HasForeignKey(d => d.DepartamentoId)
                .HasConstraintName("fk_DepartamentoId_Puestos")
                .IsRequired();

        }
    }
}
