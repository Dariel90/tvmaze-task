using TvMaze.SharedKernel.Core;

namespace TvMaze.Domain.Shows;

public interface IShowService
{
    Task<Result> GetShowInformationAsync(
        int showId,
        CancellationToken cancellationToken);
}