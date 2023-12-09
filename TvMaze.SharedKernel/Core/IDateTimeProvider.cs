namespace TvMaze.SharedKernel.Core;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}