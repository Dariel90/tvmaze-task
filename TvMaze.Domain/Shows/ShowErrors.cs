using TvMaze.SharedKernel.Core;

namespace TvMaze.Domain.Shows;

public static class ShowErrors
{
    public static Error NotFound(Guid showId) => new("Show.NotFound", $"The show with Id: {showId}, was not found");

    public static Error AlreadyExists(int showId) => new("Show.AlreadyExists", $"The show with Id: {showId}, already exists");
}