using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Configuration
{
    public class ComplementoPagoFacturasConfig : IEntityTypeConfiguration<ComplementoPagoFactura>
    {
        public void Configure(EntityTypeBuilder<ComplementoPagoFactura> builder)
        {

            builder.HasOne(d => d.ComplementoPago)
                .WithMany(p => p.ComplementoPagoFacturas)
                .HasForeignKey(d => d.ComplementoPagoId)
                .HasConstraintName("fk_facturasasociada_complementopago")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.Factura)
                .WithMany(p => p.ComplementoPagoFacturas)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("fk_facturaasociada_factura")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
