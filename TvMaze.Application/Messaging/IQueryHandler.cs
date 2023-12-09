using MediatR;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}