using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TvMaze.Persistence;
using TvMaze.Service.Api.OptionsSetup;
using Scrutor;
using TvMaze.Application.Abstractions.Data;
using TvMaze.Infrastructure.Providers;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Service.Api.Configurations;

public class InfrastructureServiceInstaller : IServiceInstaller
{
    public void Install(IServiceCollection services, IConfiguration configuration)
    {
        this.ConfigureOptions(services);

        services.Scan(selector => selector
                    .FromAssemblies(TvMaze.Infrastructure.AssemblyReference.Assembly, TvMaze.Persistence.AssemblyReference.Assembly)
                    .AddClasses(false)
                    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                    .AsMatchingInterface()
                    .WithScopedLifetime());

        this.ConfigureDatabaseContext(services, configuration);

        this.InstallBusinessInfrastructureServices(services);

        this.ConfigureHealthChecksService(services, configuration);
    }

    private void ConfigureHealthChecksService(IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        string? tvMazeShowMainInformationUrl = configuration.GetSection("ShowMainInformationUrl").Value;
        Ensure.NotNullOrEmpty(tvMazeShowMainInformationUrl);

        services.AddHealthChecks()
            .AddSqlServer(connectionString, tags: new[] { "database" });

        services.AddHealthChecks()
            .AddUrlGroup(new Uri(tvMazeShowMainInformationUrl), "Tv Maze Endpoint");

        services
            .AddHealthChecksUI()
            .AddSqlServerStorage(connectionString);
    }

    private void ConfigureOptions(IServiceCollection services)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();
    }

    private void ConfigureDatabaseContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Persistence.ApplicationDbContext>((serviceProvider, dbContextOptionsBuilder) =>
        {
            DatabaseOptions databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;
            dbContextOptionsBuilder.UseSqlServer(
                databaseOptions.ConnectionString,
                sqlServerAction =>
                {
                    sqlServerAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);
                    sqlServerAction.CommandTimeout(databaseOptions.CommandTimeout);
                    sqlServerAction.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });

            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.IsDetailedErrorsEnabled);
            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.IsSensitiveDataLoggingEnabled);
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }

    private void InstallBusinessInfrastructureServices(IServiceCollection services)
    {
        services.AddHttpClient<TvMazeApiServiceProvider>((serviceProvider, httpClient) =>
        {
            var tvMazeApiOptions = serviceProvider.GetRequiredService<IOptions<TvMazeApiOptions>>().Value;
            httpClient.BaseAddress = new Uri(tvMazeApiOptions.Url);
        })
        .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(5)
        })
        .SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }
}