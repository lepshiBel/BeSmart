using BeSmart.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeSmart.Persistence.EntityTypeConfiguration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("Test_Name");

            builder.Property(x => x.QuestionsCount)
                .IsRequired()
                .HasColumnName("Test_QuestionsCount");

            builder.HasOne(x => x.Theme)
                .WithMany(x => x.Tests)
                .HasForeignKey(x => x.ThemeId);
        }
    }
}
