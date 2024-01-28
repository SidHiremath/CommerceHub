// See https://aka.ms/new-console-template for more information

using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Environments = Microsoft.Extensions.Hosting.Environments;

var hostBuilder = Host.CreateDefaultBuilder();

var host = hostBuilder
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(
                    "Host=localhost;Port=5432;Database=postgres;Username=sileon;Password=Pass@word")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    }).Build();

var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("--- Starting database migration ---");

var serviceScopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
var config = host.Services.GetRequiredService<IConfiguration>();

var environment = config.GetValue("ASPNETCORE_ENVIRONMENT", Environments.Production);

using var scope = serviceScopeFactory.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();

context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

try
{
    // Use migrations to update the database schema
    context.Database.Migrate();

    if (environment == Environments.Development)
        // Seed initial data only in the development environment
        ConfigurationSeeder.Seed(context);

    logger.LogInformation("--- Database migration completed successfully ---");
}
catch (Exception ex)
{
    // Log any exceptions that occur during migration
    logger.LogError($"Error during database migration: {ex.Message}");
}
finally
{
    // Ensure the host is stopped regardless of success or failure
    await host.StopAsync();
}