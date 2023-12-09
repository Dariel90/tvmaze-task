using System.ComponentModel.DataAnnotations;

namespace TvMaze.Service.Api.OptionsSetup;

public class TvMazeApiOptions
{
    public const string ConfigurationSection = "TvMazeApi";

    [Required]
    public string Url { get; init; } = string.Empty;
}