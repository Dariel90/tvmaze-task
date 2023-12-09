using TvMaze.SharedKernel.Contracts.TvMazeApi;

namespace TvMaze.Application.Shows;

public sealed record ShowResponse
{
    public Guid SysId { get; set; }
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Language { get; set; }
    public List<string>? Genres { get; set; }
    public string? Status { get; set; }
    public int Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public DateTime? Premiered { get; set; }
    public DateTime? Ended { get; set; }
    public string? OfficialSite { get; set; }
    public decimal Rating { get; set; }
    public int Weight { get; set; }
    public string? Summary { get; set; }
    public long Updated { get; set; }
    public string? SelfLink { get; set; }
    public string? PreviousEpisodeLink { get; set; }
    public string? ImageMedium { get; set; }
    public string? ImageOriginal { get; set; }
    public ShowScheduleResponse? Schedule { get; set; }
    public ShowNetworkResponse? Network { get; set; }
    public ShowExternalsResponse? Externals { get; set; }
};

public sealed record ShowScheduleResponse
{
    public TimeOnly? Time { get; set; }
    public List<string>? DaysOfTheWeek { get; set; }
}

public sealed record ShowNetworkResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public Country? Country { get; set; }
    public string? OfficialSite { get; set; }
}

public sealed record ShowExternalsResponse
{
    public long TvRage { get; set; }
    public long TheTvdb { get; set; }
    public string? Imdb { get; set; }
}