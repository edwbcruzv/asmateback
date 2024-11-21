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
    public class NominaOtroPagoConfig : IEntityTypeConfiguration<NominaOtroPago>
    {

        public void Configure(EntityTypeBuilder<NominaOtroPago> builder)
        {
            builder.HasOne(d => d.Nomina)
                .WithMany(p => p.NominaOtroPagos)
                .HasForeignKey(d => d.NominaId)
                .HasConstraintName("fk_Nomina_NominaOtroPagos")
                .IsRequired();
        } 
    }
}
