using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    internal class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Word)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Card_word");

            builder.Property(a => a.Text)
                .HasMaxLength(255)
                .HasColumnName("Card_text");

            builder.Property(a => a.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("Card_imageUrl");

            builder.Property(a => a.Transctipt)
                .HasMaxLength(150)
                .HasColumnName("Card_transcript");

            builder.HasOne(a => a.Lesson)
                .WithMany(q => q.Cards)
                .HasForeignKey(a => a.LessonId);
        }
    }
}
