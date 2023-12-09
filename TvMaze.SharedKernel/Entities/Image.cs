using System.ComponentModel.DataAnnotations.Schema;

namespace TvMaze.SharedKernel.Entities;

[ComplexType]
public record Image(string Medium, string Original);