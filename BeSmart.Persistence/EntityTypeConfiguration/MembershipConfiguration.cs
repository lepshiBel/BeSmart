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

            builder.HasOne(m=>m.Course)
                .WithMany(q => q.Memberships)
                .HasForeignKey(a => a.CourseId);

            builder.HasOne(m=>m.User)
                .WithMany(u=>u.Memberships)
                .HasForeignKey(m=>m.UserId);
        }
    }
}
