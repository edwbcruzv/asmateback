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
    public class MovimientoAhorroWiseConfig : IEntityTypeConfiguration<MovimientoAhorroWise>
    {
        public void Configure(EntityTypeBuilder<MovimientoAhorroWise> builder)
        {
            builder.HasKey(m => new { m.AhorroWiseId, m.EmployeeId, m.CompanyId, m.MovimientoId });
        }
    }
}
