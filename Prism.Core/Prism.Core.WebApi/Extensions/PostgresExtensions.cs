using Microsoft.EntityFrameworkCore;
using Npgsql;
using Prism.Core.DataAccess.Database;

namespace Prism.Core.WebApi.Extensions;

public static class PostgresExtensions
{
    public static IServiceCollection AddPostgresDbContext(this IServiceCollection services, string? connectionString)
    {
        if (services is null)
            throw new ArgumentNullException(nameof(services));

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentException($"'{nameof(connectionString)}': connection string is empty.", nameof(connectionString));

        var dataSource = new NpgsqlDataSourceBuilder(connectionString)
            .EnableDynamicJson()
            .Build();

        services.AddDbContext<PrismContext>(options => options
            .UseNpgsql(dataSource, opt => opt
                .EnableRetryOnFailure(3))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }

    public static async Task MigrateDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<PrismContext>();
        try
        {
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetService<ILogger<PrismContext>>();
            logger?.LogError(ex, "Migration error.");
            throw;
        }
    }
}
