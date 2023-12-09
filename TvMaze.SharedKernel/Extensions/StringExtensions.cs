using System.Globalization;

namespace TvMaze.SharedKernel.Extensions;

public static class StringExtensions
{
    public static DateTime? StringToDateIso8601(this string @stringDate, DateTime? myResult = null)
    {
        return DateTime.TryParseExact(stringDate, "yyyy-MM-dd", null, DateTimeStyles.None, out DateTime result) ? result : myResult;
    }

    public static TimeOnly ParseTimeString(this string @stringTime)
    {
        return TimeOnly.ParseExact(@stringTime, "HH:mm", CultureInfo.InvariantCulture);
    }
}