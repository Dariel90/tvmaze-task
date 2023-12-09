namespace TvMaze.Service.Api.OptionsSetup;

public class DatabaseOptions
{
    public string? ConnectionString { get; set; } = string.Empty;
    public int MaxRetryCount { get; set; }
    public int CommandTimeout { get; set; }
    public bool IsDetailedErrorsEnabled { get; set; } = false;
    public bool IsSensitiveDataLoggingEnabled { get; set; } = false;
}