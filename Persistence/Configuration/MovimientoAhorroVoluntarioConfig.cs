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
    public class MovimientoAhorroVoluntarioConfig : IEntityTypeConfiguration<MovimientoAhorroVoluntario>
    {
        public void Configure(EntityTypeBuilder<MovimientoAhorroVoluntario> builder)
        {
            builder.HasKey(m => new { m.AhorroVoluntarioId, m.EmployeeId, m.CompanyId, m.MovimientoId });
        }
    }
}
