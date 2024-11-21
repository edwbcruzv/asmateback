﻿using Ardalis.Specification;
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
    public class SubMenuConfig : IEntityTypeConfiguration<SubMenu>
    {
        public void Configure(EntityTypeBuilder<SubMenu> builder)
        {

            builder.HasOne(d => d.Menu)
                .WithMany(p => p.SubMenus)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("fk_submenu_menu")
                .IsRequired()
                .HasPrincipalKey(d => d.Id);

        }
    }
}
