using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Persistence.Configuration
{
    public class FacturasConfig : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> builder)
        {

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_factura_copmany")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.Client)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("fk_factura_client")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.UsoCfdi)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.UsoCfdiId)
                .HasConstraintName("fk_factura_usocfdi")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.FormaPago)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.FormaPagoId)
                .HasConstraintName("fk_factura_formapago")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoMoneda)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.TipoMonedaId)
                .HasConstraintName("fk_factura_tipomoneda")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.RegimenFiscal)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.EmisorRegimenFiscalId)
                .HasConstraintName("fk_factura_emisorregimenfiscal")
                .IsRequired()
                .HasPrincipalKey(d => d.Id)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(d => d.MetodoPago)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.MetodoPagoId)
                .HasConstraintName("fk_factura_medotodopago")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.TipoComprobante)
                .WithMany(p => p.Facturas)
                .HasForeignKey(d => d.TipoComprobanteId)
                .HasConstraintName("fk_factura_tipocomprobante")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            

        }
    }
}
