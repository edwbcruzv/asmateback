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
    public class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Clients)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_company")
                .IsRequired();

            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.Clients)
                .HasForeignKey(d => d.RegimenFiscalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_regimenFiscal_cliente")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
