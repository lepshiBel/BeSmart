using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class StatusThemeConfiguration : IEntityTypeConfiguration<StatusTheme>
    {
        public void Configure(EntityTypeBuilder<StatusTheme> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Status)
                .HasDefaultValue("Не пройдено");

            builder.Property(s => s.MembershipId)
                .HasColumnName("Membersip_ID");

            builder.Property(s => s.ThemeId)
                .HasColumnName("Theme_ID");

            builder.HasOne(m => m.Membership)
                .WithMany(q => q.StatusThemes)
                .HasForeignKey(a => a.MembershipId);

            builder.HasOne(m => m.Theme)
                .WithMany(u => u.StatusThemes)
                .HasForeignKey(m => m.ThemeId);
        }
    }
}
