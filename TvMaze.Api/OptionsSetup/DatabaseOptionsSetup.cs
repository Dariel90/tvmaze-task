using Microsoft.Extensions.Options;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Service.Api.OptionsSetup;

public class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
    private const string SECTION_NAME = nameof(DatabaseOptions);

    public void Configure(DatabaseOptions options)
    {
        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);
        options.ConnectionString = connectionString;
        configuration.GetSection(SECTION_NAME).Bind(options);
    }
}