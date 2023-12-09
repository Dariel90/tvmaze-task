using MediatR;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}