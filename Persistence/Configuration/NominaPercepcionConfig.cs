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
    public class NominaPercepcionConfig : IEntityTypeConfiguration<NominaPercepcion>
    {

        public void Configure(EntityTypeBuilder<NominaPercepcion> builder)
        {
            builder.HasOne(d => d.Nomina)
                .WithMany(p => p.NominaPercepciones)
                .HasForeignKey(d => d.NominaId)
                .HasConstraintName("fk_Nomina_NominaPercepciones")
                .IsRequired();
        } 
    }
}
