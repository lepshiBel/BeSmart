using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("User_Name");

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("User_Email");

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("User_Password");
        }
    }
}
