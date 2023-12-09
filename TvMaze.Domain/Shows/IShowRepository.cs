namespace TvMaze.Domain.Shows;

public interface IShowRepository
{
    public Task<bool> IsAlreadyShowInDbAsync(
        int showId,
        CancellationToken cancellationToken = default);

    public void Insert(Show show);
}