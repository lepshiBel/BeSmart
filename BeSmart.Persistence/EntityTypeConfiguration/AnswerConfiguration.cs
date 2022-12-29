using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.Id)
                .IsUnique();

            builder.Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Answer_text");

            builder.Property(a => a.Fidelity)
                .HasColumnType("Boolean")
                .HasDefaultValue(false);

            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId);
        }
    }
}