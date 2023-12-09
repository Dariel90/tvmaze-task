using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace TvMaze.SharedKernel.Core;

public static class Ensure
{
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}