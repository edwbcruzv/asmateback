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
    public class RegimenFiscalConfig : IEntityTypeConfiguration<RegimenFiscal>
    {
        public void Configure(EntityTypeBuilder<RegimenFiscal> builder)
        {
            builder.ToTable("RegimenFiscal");

            builder.Property(e => e.RegimenFiscalCve)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();

            builder.Property(e => e.RegimenFiscalDesc).IsUnicode(false);

        }
    }
}
