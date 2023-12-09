using TvMaze.Application.Messaging;
using TvMaze.SharedKernel.Contracts.TvMazeApi;

namespace TvMaze.Application.Shows.Create;

public sealed record CreateShowCommand(TvMazeShow MazeShow)
    : ICommand<Guid>;