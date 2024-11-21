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
    public class CodigoPostaleConfig : IEntityTypeConfiguration<CodigoPostale>
    {

        public void Configure(EntityTypeBuilder<CodigoPostale> builder)
        {
            
        } 
    }
}
