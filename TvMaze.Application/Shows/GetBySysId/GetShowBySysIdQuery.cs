using TvMaze.Application.Messaging;

namespace TvMaze.Application.Shows.GetBySysId;

public sealed record GetShowBySysIdQuery(Guid SysShowId) : IQuery<ShowResponse>;