using BeSmart.Server.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Server.Persistence.EntityTypeConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Password)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(a => a.Role)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
