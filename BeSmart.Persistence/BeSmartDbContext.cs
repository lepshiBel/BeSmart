using BeSmart.Domain.Models;
using BeSmart.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence
{
    public class BeSmartDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public BeSmartDbContext(DbContextOptions<BeSmartDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new AnswerConfiguration());
            base.OnModelCreating(builder);
        }
    }
}