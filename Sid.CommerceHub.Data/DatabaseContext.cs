using Microsoft.EntityFrameworkCore;

namespace Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<ProductData> ProductData { get; set; }
    public DbSet<OrderData> OrderData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductData>();
        modelBuilder.Entity<OrderData>();
    }
}