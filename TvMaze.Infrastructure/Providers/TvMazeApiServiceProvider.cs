using System.Net.Http.Json;
using TvMaze.SharedKernel.Contracts.TvMazeApi;

namespace TvMaze.Infrastructure.Providers;

public class TvMazeApiServiceProvider(HttpClient client)
{
    public Task<TvMazeShow?> GetTvMazeShowDataAsync(int showId, CancellationToken cancellationToken = default)
    {
        return client.GetFromJsonAsync<TvMazeShow>($"shows/{showId}", cancellationToken);
    }
}