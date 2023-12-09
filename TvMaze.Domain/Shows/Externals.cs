using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.Domain.Shows;

[ComplexType]
public record Externals(int TvRage, int TheTvDb, string Imdb);