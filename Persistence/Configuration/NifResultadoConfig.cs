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
    public class NifResultadoConfig : IEntityTypeConfiguration<NifResultado>
    {
        
        public void Configure(EntityTypeBuilder<NifResultado> builder)
        {
            builder.ToTable("NifResultado");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Nif)
            .WithMany(x => x.Nifresultados)
            .HasForeignKey(x => x.NifId)
            .HasConstraintName("fk_nif_nifResultados")
            .IsRequired();
        }
    }
}
