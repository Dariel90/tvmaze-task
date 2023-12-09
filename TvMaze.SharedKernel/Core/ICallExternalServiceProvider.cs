using TvMaze.SharedKernel.Contracts.TvMazeApi;

namespace TvMaze.SharedKernel.Core;

public interface ICallExternalServiceProvider
{
    Task<TvMazeShow?> GetTvMazeShowDataAsync(int showId, CancellationToken cancellationToken = default);
}