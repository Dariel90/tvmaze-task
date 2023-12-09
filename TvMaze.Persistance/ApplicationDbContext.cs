using Microsoft.EntityFrameworkCore;
using TvMaze.Application.Abstractions.Data;
using TvMaze.Domain.Networks;
using TvMaze.Domain.Shows;

namespace TvMaze.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        base.OnModelCreating(builder);
    }

    public DbSet<Show> Shows { get; set; }
    public DbSet<Network> Networks { get; set; }
}