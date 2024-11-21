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
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(e => e.Calle).IsUnicode(false);

            builder.Property(e => e.Certificado).IsUnicode(false);

            builder.Property(e => e.Colonia).IsUnicode(false);

            builder.Property(e => e.Estado).IsUnicode(false);

            builder.Property(e => e.Municipio).IsUnicode(false);

            builder.Property(e => e.NoExt).IsUnicode(false);

            builder.Property(e => e.NoInt).IsUnicode(false);

            builder.Property(e => e.Pais).IsUnicode(false);

            builder.Property(e => e.PassPrivateKey).IsUnicode(false);

            builder.Property(e => e.PrivateKeyFile).IsUnicode(false);

            builder.Property(e => e.RazonSocial).IsUnicode(false);

            builder.Property(e => e.Rfc).IsUnicode(false);

            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.Companies)
                .HasForeignKey(d => d.RegimenFiscalId)
                .HasConstraintName("fk_regimenFiscal_companies")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
