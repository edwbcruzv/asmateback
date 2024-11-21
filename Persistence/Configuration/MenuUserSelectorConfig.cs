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
    public class MenuUserSelectorConfig : IEntityTypeConfiguration<MenuUserSelector>
    {
        public void Configure(EntityTypeBuilder<MenuUserSelector> builder)
        {

            builder.HasOne(d => d.Menu)
                .WithMany(p => p.MenuUserSelectors)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("fk_menuuser_menu")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

            builder.HasOne(d => d.User)
                .WithMany(p => p.MenuUserSelectors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_menuuser_user")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
