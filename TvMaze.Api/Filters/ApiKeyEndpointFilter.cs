﻿using Microsoft.AspNetCore.Mvc;

namespace TvMaze.Service.Api.Filters;

public class ApiKeyEndpointFilter : IEndpointFilter
{
    private const string API_KEY = "Api_Key";

    private ILogger<ApiKeyEndpointFilter> _logger;
    private readonly IConfiguration _configuration;

    public ApiKeyEndpointFilter(ILoggerFactory loggerFactory, IConfiguration configuration)
    {
        _logger = loggerFactory.CreateLogger<ApiKeyEndpointFilter>();
        _configuration = configuration;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext,
        EndpointFilterDelegate next)
    {
        bool success = efiContext.HttpContext.Request.Headers.TryGetValue(API_KEY, out var apiKeyFromHttpHeader);
        if (!success)
        {
            _logger.LogInformation("Unauthorized");
            return Results.Problem(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Detail = "The Api Key for accessing this endpoint is not available"
            });
        }

        string? apiKeyValue = _configuration.GetSection(API_KEY).Value;

        if (apiKeyValue is null || !apiKeyValue.Equals(apiKeyFromHttpHeader))
        {
            _logger.LogInformation("Unauthorized");
            return Results.Problem(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Detail = "The Api key is incorrect : Unauthorized access"
            });
        }
        _logger.LogInformation("Successful authorization");
        return await next(efiContext);
    }
}