using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(m=>m.Id);

            builder.Property(m => m.Status)
                .HasDefaultValue("Не пройдено");
            
            builder.Property(s => s.AmountOfCompletedThemes)
                .HasColumnName("Amount_of_completed_themes")
                .HasDefaultValue(0);

            builder.HasOne(m=>m.Course)
                .WithMany(q => q.Memberships)
                .HasForeignKey(a => a.CourseId);

            builder.HasOne(m=>m.User)
                .WithMany(u=>u.Memberships)
                .HasForeignKey(m=>m.UserId);
        }
    }
}
