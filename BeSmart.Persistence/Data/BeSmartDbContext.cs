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
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new LessonConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CardConfiguration());
        modelBuilder.ApplyConfiguration(new AccountTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CourseConfiguration());
        modelBuilder.ApplyConfiguration(new ThemeConfiguration());
        modelBuilder.ApplyConfiguration(new TestConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}