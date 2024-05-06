using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Shows;

namespace TvMaze.Persistence.Repositories;

public class ShowRepository(ApplicationDbContext dbContext) : IShowRepository
{
    public Task<bool> IsAlreadyShowInDbAsync(int showId, CancellationToken cancellationToken = default)
    {
        return dbContext.Shows.AnyAsync(s => s.Id == showId, cancellationToken);
    }

    public void Insert(Show show)
    {
        dbContext.Shows.Add(show);
    }
}