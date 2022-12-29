using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Text)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Question_Text");

            builder.HasOne(x => x.Test)
                .WithMany(x => x.Questsions)
                .HasForeignKey(x => x.TestId);
        }
    }
}