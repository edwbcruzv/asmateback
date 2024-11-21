using Ardalis.Specification;
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
    public class DepartamentoConfig : IEntityTypeConfiguration<Departamento>
    {
        public void Configure(EntityTypeBuilder<Departamento> builder)
        {
           

            builder.HasOne(d => d.Company)
                .WithMany(p => p.Departamentos)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_CompanyId_Departamentos")
                .IsRequired();
        }
    }
}
