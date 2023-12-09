using Microsoft.EntityFrameworkCore;

namespace TvMaze.SharedKernel.Entities;

[Owned]
public record Genre(string Category);