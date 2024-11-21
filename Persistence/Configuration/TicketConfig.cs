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
    public class TicketConfig : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasOne(t => t.EmployeeAsignado)
                .WithMany(e => e.TicketsAsignados)
                .HasForeignKey(t => t.EmployeeAsignadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.EmployeeCreador)
                .WithMany(e => e.TicketsCreados)
                .HasForeignKey(t => t.EmployeeCreadorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
