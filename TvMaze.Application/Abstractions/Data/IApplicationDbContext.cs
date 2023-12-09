using Microsoft.EntityFrameworkCore;
using TvMaze.Domain.Networks;
using TvMaze.Domain.Shows;

namespace TvMaze.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Show> Shows { get; set; }
    DbSet<Network> Networks { get; set; }
}