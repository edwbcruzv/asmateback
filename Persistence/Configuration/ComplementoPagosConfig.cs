using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Configuration
{
    public class ComplementoPagosConfig : IEntityTypeConfiguration<ComplementoPago>
    {
        public void Configure(EntityTypeBuilder<ComplementoPago> builder)
        {

            builder.HasOne(d => d.Company)
                .WithMany(p => p.ComplementoPagos)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_complementopago_copamany")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.Client)
                .WithMany(p => p.ComplementoPagos)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_complementopago_client")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.FormaPago)
                .WithMany(p => p.ComplementoPagos)
                .HasForeignKey(d => d.FormaPagoId)
                .HasConstraintName("fk_complementopago_formapago")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoMoneda)
                .WithMany(p => p.ComplementoPagos)
                .HasForeignKey(d => d.TipoMonedaId)
                .HasConstraintName("fk_complementopago_tipomoneda")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.ComplementoPagos)
                .HasForeignKey(d => d.EmisorRegimenFiscalId)
                .HasConstraintName("fk_complementopago_emisorregimenfiscal")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
