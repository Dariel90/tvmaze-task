using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Shows;

namespace TvMaze.Persistence.Repositories;

public class ShowRepository : IShowRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ShowRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> IsAlreadyShowInDbAsync(int showId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Shows.AnyAsync(s => s.Id == showId, cancellationToken);
    }

    public void Insert(Show show)
    {
        _dbContext.Shows.Add(show);
    }
}