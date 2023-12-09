using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Abstractions.Data;
using TvMaze.Application.Messaging;
using TvMaze.Domain.Shows;
using TvMaze.SharedKernel.Contracts.TvMazeApi;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Shows.GetBySysId;

internal sealed class GetShowBySysIdQueryHandler : IQueryHandler<GetShowBySysIdQuery, ShowResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetShowBySysIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ShowResponse>> Handle(GetShowBySysIdQuery query, CancellationToken cancellationToken)
    {
        ShowResponse? showResponse = await _dbContext.Shows
            .Include(s => s.Genres)
            .Include(s => s.Schedules)
            .Include(s => s.Network)
            .AsNoTracking()
            .Where(u => u.SysId == query.SysShowId)
            .Select(u => GetShowResponse(u))
            .FirstOrDefaultAsync(cancellationToken);

        if (showResponse is null)
        {
            return Result.Failure<ShowResponse>(ShowErrors.NotFound(query.SysShowId));
        }

        return showResponse;
    }

    private static ShowResponse GetShowResponse(Show show)
    {
        return new ShowResponse
        {
            Id = show.Id,
            SysId = show.SysId,
            Name = show.Name,
            OfficialSite = show.OfficialSite,
            AverageRuntime = show.AverageRuntime,
            Ended = show.Ended,
            ImageMedium = show.Image.Medium,
            ImageOriginal = show.Image.Original,
            Language = show.Language,
            PreviousEpisodeLink = show.Links.PreviousEpisode.Href,
            SelfLink = show.Links.Self.Href,
            Rating = show.Rating.Average,
            Runtime = show.Runtime,
            Status = show.Status,
            Weight = show.Weight,
            Summary = show.Summary,
            Premiered = show.Premiered,
            Updated = show.Updated,
            Url = show.Url,
            Type = show.Type,
            Genres = show.Genres.Select(g => g.Category).ToList(),
            Network = new ShowNetworkResponse
            {
                Id = show.Network.Id,
                Name = show.Network.Name,
                OfficialSite = show.Network.OfficialSite,
                Country = new Country(show.Network.Country.Name, show.Network.Country.Code, show.Network.Country.Timezone)
            },
            Schedule = new ShowScheduleResponse
            {
                Time = show.Schedules.FirstOrDefault()?.Time,
                DaysOfTheWeek = show.Schedules.Select(s => s.DayOfTheWeek.ToString()).ToList()
            },
            Externals = new ShowExternalsResponse
            {
                Imdb = show.Externals.Imdb,
                TheTvdb = show.Externals.TheTvDb,
                TvRage = show.Externals.TvRage,
            }
        };
    }
}