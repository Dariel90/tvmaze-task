using TvMaze.SharedKernel.Contracts.TvMazeApi;
using TvMaze.SharedKernel.Entities;
using TvMaze.SharedKernel.Extensions;
using Image = TvMaze.SharedKernel.Entities.Image;
using Network = TvMaze.Domain.Networks.Network;
using Rating = TvMaze.SharedKernel.Entities.Rating;

namespace TvMaze.Domain.Shows;

public class Show
{
    public Guid SysId { get; set; }
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Language { get; set; }
    public List<Genre> Genres { get; set; }
    public string? Status { get; set; }
    public int Runtime { get; set; }
    public int AverageRuntime { get; set; }
    public DateTime? Premiered { get; set; }
    public DateTime? Ended { get; set; }
    public string? OfficialSite { get; set; }
    public List<Schedule> Schedules { get; set; }
    public Rating Rating { get; set; }
    public int Weight { get; set; }
    public Guid NetworkSysId { get; set; }
    public Network Network { get; set; }
    public string? WebChannel { get; set; }
    public string? DvdCountry { get; set; }
    public Externals Externals { get; set; }
    public Image Image { get; set; }
    public string? Summary { get; set; }
    public long Updated { get; set; }
    public Link Links { get; set; }

    public static Show Create(Guid sysId, TvMazeShow mazeShow)
    {
        List<Schedule> schedules = GetTvMazeShowSchedule(mazeShow.schedule);
        List<Genre> genres = GetTvShowGenres(mazeShow.genres);
        var show = new Show
        {
            Id = mazeShow.id,
            AverageRuntime = mazeShow.averageRuntime,
            Ended = mazeShow.ended.StringToDateIso8601(null),
            Externals = new Externals(mazeShow.externals.tvrage, mazeShow.externals.thetvdb, mazeShow.externals.imdb),
            Image = new Image(mazeShow.image.medium, mazeShow.image.original),
            Summary = mazeShow.summary,
            Updated = mazeShow.updated,
            Language = mazeShow.language,
            Name = mazeShow.name,
            OfficialSite = mazeShow.officialSite,
            Premiered = mazeShow.premiered.StringToDateIso8601(null),
            Runtime = mazeShow.runtime,
            Status = mazeShow.status,
            SysId = sysId,
            Type = mazeShow.type,
            Url = mazeShow.url,
            Weight = mazeShow.weight,
            Network = new Network
            {
                Id = mazeShow.network.id,
                Name = mazeShow.network.name,
                SysId = Guid.NewGuid(),
                OfficialSite = mazeShow.officialSite,
                Country = new SharedKernel.Entities.Country(mazeShow.network.country.name, mazeShow.network.country.code, mazeShow.network.country.timezone)
            },
            Links = new Link(mazeShow._links.self.href, mazeShow._links.previousepisode.href),
            Rating = new Rating(mazeShow.rating.average),
            Schedules = schedules,
            Genres = genres,
        };

        return show;
    }

    private static List<Schedule> GetTvMazeShowSchedule(SharedKernel.Contracts.TvMazeApi.Schedule mazeShowSchedule)
    {
        if (mazeShowSchedule.days.Length == 0)
            return new List<Schedule>();

        var time = mazeShowSchedule.time.ParseTimeString();
        return mazeShowSchedule.days.ToList()
            .Select(schedule =>
                (DayOfWeek)Enum.Parse(typeof(DayOfWeek), schedule))
            .Select(dayOfTheWeek => new Schedule(time, dayOfTheWeek))
            .ToList();
    }

    private static List<Genre> GetTvShowGenres(string[] mazeShowGenres)
    {
        return mazeShowGenres.Length == 0
            ? new List<Genre>()
            : mazeShowGenres.ToList().Select(genre => new Genre(genre)).ToList();
    }
}