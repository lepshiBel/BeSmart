using BeSmart.Server.Persistence.EntityTypeConfiguration;
using BeSmart.Server.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BeSmart.Server.Persistence.Data;

public class BeSmartDbContext : DbContext
{
    public BeSmartDbContext(DbContextOptions options) : base(options) {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}