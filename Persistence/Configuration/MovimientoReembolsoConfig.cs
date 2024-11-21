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
    public class MovimientoReembolsoConfig : IEntityTypeConfiguration<MovimientoReembolso>
    {
        public void Configure(EntityTypeBuilder<MovimientoReembolso> builder)
        {
            builder.HasOne(a => a.TipoImpuesto)
                .WithMany(b => b.MovimientosReembolso) // TipoImpuesto
                .HasForeignKey(a => a.TipoImpuestoId)
                .HasConstraintName("fk_id_tipoimpuesto_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.TipoMoneda)
                .WithMany(b => b.MovimientosReembolso) // TipoMoneda
                .HasForeignKey(a => a.TipoMonedaId)
                .HasConstraintName("fk_id_tipomoneda_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(a => a.TipoCambio)
                .IsRequired(false);

            builder.HasOne(a => a.TipoComprobante)
                .WithMany(b => b.MovimientosReembolso) // TipoComprobante
                .HasForeignKey(a => a.TipoComprobanteId)
                .HasConstraintName("fk_id_tipocomprobante_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Reembolso)
                .WithMany(b => b.MovimientosReembolso) // Reembolso
                .HasForeignKey(a => a.ReembolsoId)
                .HasConstraintName("fk_id_reembolso_movreembolso")
                .IsRequired();

            builder.HasOne(a => a.FormaPago)
                .WithMany(b => b.MovimientosReembolso) // FormaPago
                .HasForeignKey(a => a.FormaPagoId)
                .HasConstraintName("fk_id_formapago_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.TipoReembolso)
                .WithMany(b => b.MovimientosReembolso) // TipoReembolso
                .HasForeignKey(a => a.TipoReembolsoId)
                .HasConstraintName("fk_id_tiporeembolso_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.MetodoPago)
                .WithMany(b => b.MovimientosReembolso) // MetodoPago
                .HasForeignKey(a => a.MetodoPagoId)
                .HasConstraintName("fk_id_metodopago_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.RegimenFiscal)
                .WithMany(b => b.MovimientosReembolso) // RegimenFiscal
                .HasConstraintName("fk_id_regimenfiscal_movreembolso")
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
