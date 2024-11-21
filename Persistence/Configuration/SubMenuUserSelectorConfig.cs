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
    public class SubMenuUserSelectorConfig : IEntityTypeConfiguration<SubMenuUserSelector>
    {
        public void Configure(EntityTypeBuilder<SubMenuUserSelector> builder)
        {

            builder.HasOne(d => d.SubMenu)
                .WithMany(p => p.SubMenuUserSelectors)
                .HasForeignKey(d => d.SubMenuId)
                .HasConstraintName("fk_submenuuser_submenu")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.User)
                .WithMany(p => p.SubMenuUserSelectors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_submenuuser_user")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
