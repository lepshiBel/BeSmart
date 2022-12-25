using BeSmart.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Persistence.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options) {}

    public DbSet<Course> Courses { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Theme> Themes { get; set; }
    
    public DbSet<Lesson> Lessons { get; set; }
    
    public DbSet<Test> Tests { get; set; }
    
    public DbSet<Card> Cards { get; set; }

    public DbSet<Questsion> Questsions { get; set; }

    public DbSet<Anwser> Anwsers { get; set; }
}