using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data;

public class DatabaseFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        return CreateDbContext();
    }

    public DatabaseContext CreateDbContext()
    {
        return new DatabaseContext(CreateDbContextOptions());
    }

    public DbContextOptions<DatabaseContext> CreateDbContextOptions()
    {
        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

        optionsBuilder
            .UseNpgsql(
                "Host=localhost;Port=5432;Database=postgres;Username=sileon;Password=Pass@word")
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        return optionsBuilder.Options;
    }
}