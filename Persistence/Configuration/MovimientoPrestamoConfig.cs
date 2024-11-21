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
    public class MovimientoPrestamoConfig : IEntityTypeConfiguration<MovimientoPrestamo>
    {
        public void Configure(EntityTypeBuilder<MovimientoPrestamo> builder)
        {
            builder.HasKey(m => new { m.PrestamoId, m.EmployeeId, m.CompanyId, m.MovimientoId });
        }
    }
}
