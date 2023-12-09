using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TvMaze.Persistence.Database;

public static class SeedDatabase
{
    public static async Task InitializeDataAsync(IServiceProvider servicesProvider)
    {
        using IServiceScope serviceScope = servicesProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        ApplicationDbContext? context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        if (context == null)
            return;
        IEnumerable<string> pendingMigrations = await context.Database.GetPendingMigrationsAsync();
        if (pendingMigrations.Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}