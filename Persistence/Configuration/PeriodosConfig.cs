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
    public class PeriodosConfig : IEntityTypeConfiguration<Periodo>
    {
        public void Configure(EntityTypeBuilder<Periodo> builder)
        {
            builder.HasOne(d => d.Company)
                .WithMany(p => p.Periodos)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_CompanyId_Periodos")
                .IsRequired();

            builder.HasOne(d => d.TipoPeriocidadPago)
                .WithMany(p => p.Periodos)
                .HasForeignKey(d => d.TipoPeriocidadPagoId)
                .HasConstraintName("fk_TipoPeriocidadPagoId_Periodos")
                .IsRequired();

        }
    }
}

