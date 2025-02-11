using Microsoft.EntityFrameworkCore;
using HydroSpark.Models;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Define DbSets for your entities
    public DbSet<User> Users { get; set; }
    public DbSet<Employee> Employee { get; set; }

    // Optionally, you can override OnModelCreating for additional configurations
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>()
           .HasOne(r => r.Employee)
           .WithMany(e => e.Roles)
           .HasForeignKey(r => r.EmployeeId);
        // You can configure relationships, indexes, etc. here
    }
}
