using MediatR;
using TvMaze.Application.Shows;
using TvMaze.Application.Shows.Create;
using TvMaze.Application.Shows.GetBySysId;
using TvMaze.Infrastructure.Providers;
using TvMaze.Service.Api.Extensions;
using TvMaze.Service.Api.Filters;
using TvMaze.SharedKernel.Core;

namespace TvMaze.Service.Api.Endpoints;

public static class ShowEndpoints
{
    public static void MapShowEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("shows/{showId}", async (
            int showId,
            TvMazeApiServiceProvider tvMazeApiService) =>
        {
            var content = await tvMazeApiService.GetTvMazeShowDataAsync(showId);

            return Results.Ok(content);
        });

        app.MapPost("api/shows/{showId}/create", async (int showId, ISender sender, TvMazeApiServiceProvider tvMazeApiService) =>
        {
            var content = await tvMazeApiService.GetTvMazeShowDataAsync(showId);

            if (content == null)
                return Results.NotFound(content);

            var command = new CreateShowCommand(content);
            Result<Guid> result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        }).AddEndpointFilter<ApiKeyEndpointFilter>();

        app.MapGet("api/shows/{sysShowId}", async (
            Guid sysShowId,
            ISender sender) =>
        {
            Result<ShowResponse> result = await sender.Send(new GetShowBySysIdQuery(sysShowId));

            return Results.Ok(result);
        });
    }
}