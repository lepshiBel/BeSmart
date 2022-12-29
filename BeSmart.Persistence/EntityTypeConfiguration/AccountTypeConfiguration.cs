using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Id)
                .IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("AccountType_Name");
                
            builder.Property(x => x.Description)
                .HasMaxLength(150)
                .HasColumnName("AccoutType_Description");
        }
    }
}
