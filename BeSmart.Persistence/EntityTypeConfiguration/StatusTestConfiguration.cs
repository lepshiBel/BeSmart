using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class StatusTestConfiguration : IEntityTypeConfiguration<StatusTest>
    {
        public void Configure(EntityTypeBuilder<StatusTest> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Mark)
                .HasDefaultValue(null);

            builder.Property(s => s.AmountOfFaithfullAnswers)
                .HasColumnName("Amount_of_faithfull_answers")
                .HasDefaultValue(0);

            builder.Property(s => s.AmountOfIncorrectAnswers)
                .HasColumnName("Amount_of_incorrect_answers")
                .HasDefaultValue(0);

            builder.Property(s => s.Status)
                .HasDefaultValue("Не пройдено");

            builder.Property(s => s.StatusThemeId)
                .HasColumnName("Status_of_theme_ID");

            builder.Property(s => s.TestId)
                .HasColumnName("Test_ID");

            builder.HasOne(m => m.StatusTheme)
                .WithMany(q => q.StatusTests)
                .HasForeignKey(a => a.StatusThemeId);

            builder.HasOne(m => m.Test)
                .WithMany(u => u.StatusTests)
                .HasForeignKey(m => m.TestId);
        }
    }
}
