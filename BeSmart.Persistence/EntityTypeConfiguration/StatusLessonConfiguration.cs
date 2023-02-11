using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class StatusLessonConfiguration : IEntityTypeConfiguration<StatusLesson>
    {
        public void Configure(EntityTypeBuilder<StatusLesson> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Status)
                .HasDefaultValue("Не пройдено");

            builder.Property(s => s.StatusThemeId)
                .HasColumnName("Status_of_theme_ID");

            builder.Property(s => s.LessonId)
                .HasColumnName("Lesson_ID");

            builder.HasOne(m => m.StatusTheme)
                .WithMany(q => q.StatusLessons)
                .HasForeignKey(a => a.StatusThemeId);

            builder.HasOne(m => m.Lesson)
                .WithMany(u => u.StatusLessons)
                .HasForeignKey(m => m.LessonId);
        }
    }
}
