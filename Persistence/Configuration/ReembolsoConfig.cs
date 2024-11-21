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
    public class ReembolsoConfig : IEntityTypeConfiguration<Reembolso>
    {
        public void Configure(EntityTypeBuilder<Reembolso> builder)
        {
            
            builder.HasOne(a => a.Company)
                .WithMany(b => b.Rembolsos) // Company
                .HasForeignKey(a => a.CompanyId)
                .HasConstraintName("fk_id_company_reembolso")
                .IsRequired();

            builder.HasOne(a => a.TipoEstatusReembolso)
                .WithMany(b => b.Reembolsos) // TipoEstatusReembolso
                .HasForeignKey(a => a.EstatusId)
                .HasConstraintName("fk_id_estatus_reembolso");

            builder.HasOne(a => a.User)
                .WithMany(b => b.Reembolsos) // User
                .HasForeignKey(a => a.UsuarioIdPago)
                .HasConstraintName("fk_id_user_reembolso")
                .IsRequired();
        }
    }
}
