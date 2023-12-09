using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.SharedKernel.Entities;

[ComplexType]
public record Rating(decimal Average);