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
    public class RetiroAhorroVoluntarioConfig : IEntityTypeConfiguration<RetiroAhorroVoluntario>
    {
        public void Configure(EntityTypeBuilder<RetiroAhorroVoluntario> builder)
        {
            builder.Property(m => m.Id).ValueGeneratedOnAdd();    
            builder.HasKey(m => new {m.Id,  m.AhorroVoluntarioId });
        }
    }
}
