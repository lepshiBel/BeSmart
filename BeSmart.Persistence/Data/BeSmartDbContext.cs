using BeSmart.Domain.Models;
using BeSmart.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Data;

public class BeSmartDbContext : DbContext
{
    public BeSmartDbContext(DbContextOptions options) : base(options) {}

    public DbSet<Course> Courses { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Theme> Themes { get; set; }
    
    public DbSet<Lesson> Lessons { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Card> Cards { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}