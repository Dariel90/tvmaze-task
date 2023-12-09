using System.Net.Http.Json;
using TvMaze.SharedKernel.Contracts.TvMazeApi;

namespace TvMaze.Infrastructure.Providers;

public class TvMazeApiServiceProvider
{
    private readonly HttpClient _client;

    public TvMazeApiServiceProvider(HttpClient client)
    {
        _client = client;
    }

    public Task<TvMazeShow?> GetTvMazeShowDataAsync(int showId, CancellationToken cancellationToken = default)
    {
        return _client.GetFromJsonAsync<TvMazeShow>($"shows/{showId}", cancellationToken);
    }
}