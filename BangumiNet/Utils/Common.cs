using System;

namespace BangumiNet.Utils;

public static class Common
{
    /// <summary>
    /// Parse date string to <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="date"><c>yyyy-MM-DD</c></param>
    /// <returns></returns>
    public static DateOnly? ParseDate(string? date)
    {
        if (DateOnly.TryParseExact(date, "", out DateOnly result))
            return result;
        else return null;
    }

    public static DayOfWeek? ParseDayOfWeek(int? day, DayOfWeek startingDay = DayOfWeek.Monday, int startingIndex = 1)
    {
        if (day is not { } d) return null;

        int i = d - startingIndex + (int)startingDay;
        if (i > 6) i -= 7;
        return (DayOfWeek)i;
    }
}
