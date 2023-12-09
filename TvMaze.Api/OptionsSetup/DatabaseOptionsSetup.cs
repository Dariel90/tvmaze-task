using Microsoft.Extensions.Options;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Service.Api.OptionsSetup;

public class DatabaseOptionsSetup : IConfigureOptions<DatabaseOptions>
{
    private const string SECTION_NAME = nameof(DatabaseOptions);
    private readonly IConfiguration configuration;

    public DatabaseOptionsSetup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public void Configure(DatabaseOptions options)
    {
        string? connectionString = this.configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);
        options.ConnectionString = connectionString;
        this.configuration.GetSection(SECTION_NAME).Bind(options);
    }
}