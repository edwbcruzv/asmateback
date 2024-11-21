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
    public class ContractsUserCompaniesConfig : IEntityTypeConfiguration<ContractsUserCompany>
    {
        public void Configure(EntityTypeBuilder<ContractsUserCompany> builder)
        {
            
            builder.HasOne(d => d.Company)
                .WithMany(p => p.ContractsUserCompanies)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_usercompany_company")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.User)
                .WithMany(p => p.ContractsUserCompanies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_usercompany_user")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
