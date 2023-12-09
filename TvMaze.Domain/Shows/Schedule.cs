using Microsoft.EntityFrameworkCore;

namespace TvMaze.Domain.Shows;

[Owned]
public record Schedule(TimeOnly Time, DayOfWeek DayOfTheWeek);