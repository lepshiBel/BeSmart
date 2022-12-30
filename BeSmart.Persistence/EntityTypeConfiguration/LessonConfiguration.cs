using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    internal class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Lesson_name");

            builder.Property(a => a.Text)
                .HasMaxLength(255)
                .HasColumnName("Lesson_text");

            builder.HasOne(a => a.Theme)
                .WithMany(q => q.Lessons)
                .HasForeignKey(a => a.ThemeId);
        }
    }
}
