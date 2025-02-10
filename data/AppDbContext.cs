using Microsoft.EntityFrameworkCore;
using HydroSpark.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define DbSets for your entities
    public DbSet<User> Users { get; set; }

    // Optionally, you can override OnModelCreating for additional configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // You can configure relationships, indexes, etc. here
    }
}
