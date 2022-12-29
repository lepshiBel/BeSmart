using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.Id)
                .IsUnique();

            builder.Property(a => a.UserName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Account_userName");

            builder.Property(a => a.UserEmail)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Account_userEmail");

            builder.Property(a => a.UserPassword)
                .IsRequired()
                .HasMaxLength(100)   
                .HasColumnName("Account_userPassword");

            builder.HasOne(a => a.AccountType)
                .WithMany(q => q.Accounts)
                .HasForeignKey(a => a.AccountTypeId);
        }
    }
}
