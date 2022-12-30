using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.HasKey(x => x.Id);

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
