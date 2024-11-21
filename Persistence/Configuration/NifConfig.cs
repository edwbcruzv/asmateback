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
    public class NifConfig : IEntityTypeConfiguration<Nif>
    {
        public void Configure(EntityTypeBuilder<Nif> builder)
        {
            builder.ToTable("Nif");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Company)
            .WithMany(x => x.Nifs)
            .HasForeignKey(x => x.CompanyId)
            .HasConstraintName("fk_company_nif")
            .IsRequired();

        }
    }

    
}
