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
    public class UsersConfig : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.Property(e => e.UserName).IsUnicode(false);
            
            builder.Property(e => e.NickName).IsUnicode(false);
            
            builder.Property(e => e.UserPassword).IsUnicode(false);
            
            builder.Property(e => e.UserProfile).IsUnicode(false);
            
            builder.Property(e => e.UserEmail).IsUnicode(false);
            
            builder.Property(e => e.UserType).IsUnicode(false);

        } 
    }
}
