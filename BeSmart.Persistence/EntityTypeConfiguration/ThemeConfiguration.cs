using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class ThemeConfiguration : IEntityTypeConfiguration<Theme>
    {
        public void Configure(EntityTypeBuilder<Theme> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Theme_Name");

            builder.Property(x => x.CountLesson)
                .IsRequired()
                .HasColumnName("Theme_CountLesson");

            builder.Property(x => x.CountTest)
                .IsRequired()
                .HasColumnName("Theme_CountTest");

            builder.HasOne(x => x.Course)
                .WithMany(x => x.CourseThemes)
                .HasForeignKey(x => x.CourseId);

        }
    }
}
