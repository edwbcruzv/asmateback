using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class FacturaMovimientosConfig : IEntityTypeConfiguration<FacturaMovimiento>
    {

        public void Configure(EntityTypeBuilder<FacturaMovimiento> builder)
        {
            builder.Property(f => f.Descuento)
                .HasColumnType("decimal(18,4)");

            builder.Property(f => f.PrecioUnitario)
                .HasColumnType("decimal(18,4)");

            builder.HasOne(d => d.CveProducto)
                .WithMany(p => p.FacturaMovimientos)
                .HasForeignKey(d => d.CveProductoId)
                .HasConstraintName("fk_facturamovimiento_CveProducto")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.ObjetoImpuesto)
                .WithMany(p => p.FacturaMovimientos)
                .HasForeignKey(d => d.ObjetoImpuestoId)
                .HasConstraintName("fk_facturamovimiento_ObjetoImpuesto")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.UnidadMedida)
                .WithMany(p => p.FacturaMovimientos)
                .HasForeignKey(d => d.UnidadMedidaId)
                .HasConstraintName("fk_facturamovimiento_UnidadMedida")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.Factura)
                .WithMany(p => p.FacturaMovimientos)
                .HasForeignKey(d => d.FacturaId)
                .HasConstraintName("fk_facturamovimiento_Factura")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);
        } 
    }
}
