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
    public class NominaDeduccionConfig : IEntityTypeConfiguration<NominaDeduccion>
    {

        public void Configure(EntityTypeBuilder<NominaDeduccion> builder)
        {
            builder.HasOne(d => d.Nomina)
                .WithMany(p => p.NominaDeducciones)
                .HasForeignKey(d => d.NominaId)
                .HasConstraintName("fk_Nomina_NominaDeducciones")
                .IsRequired();
        } 
    }
}
