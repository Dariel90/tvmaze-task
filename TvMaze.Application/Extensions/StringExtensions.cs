using System.Globalization;

namespace TvMaze.Application.Extensions;

public static class StringExtensions
{
    public static DateTime? StringToDateIso8601(this string @stringDate, DateTime? myResult = null)
    {
        return DateTime.TryParseExact(stringDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime result) ? result : myResult;
    }
}