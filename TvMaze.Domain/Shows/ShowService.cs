using TvMaze.SharedKernel.Core;

namespace TvMaze.Domain.Shows;

public class ShowService : IShowService
{
    public async Task<Result> GetShowInformationAsync(int showId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}