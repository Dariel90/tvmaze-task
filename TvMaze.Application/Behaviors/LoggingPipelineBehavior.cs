using MediatR;
using Microsoft.Extensions.Logging;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Application.Behaviors;

public class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        this.logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Starting request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        TResponse result = await next();

        if (result.IsFailure)
        {
            this.logger.LogError("Request failure {@RequestName}, {@Error} {@DateTimeUtc}", typeof(TRequest).Name, result.Errors[0], DateTime.UtcNow);
        }

        this.logger.LogInformation("Completed request {@RequestName}, {@DateTimeUtc}", typeof(TRequest).Name, DateTime.UtcNow);

        return result;
    }
}