using System.Diagnostics;

namespace BangumiNet.Utils;

public static class Common
{
    /// <summary>
    /// 将 Bangumi 的日期 string 转换为 <see cref="DateOnly"/>.
    /// </summary>
    /// <param name="date"><c>yyyy-MM-dd</c></param>
    public static DateOnly? ParseBangumiDate(string? date)
    {
        if (DateOnly.TryParseExact(date, "yyyy-MM-dd", out DateOnly result))
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

    public static void OpenUrlInBrowser(string url)
        => Process.Start(new ProcessStartInfo()
        {
            FileName = url,
            UseShellExecute = true,
        });

    public static int? NumberToInt(object? number)
    {
        if (number is decimal de)
            return decimal.ConvertToInteger<int>(de);
        else if (number is double d)
            return double.ConvertToInteger<int>(d);
        else if (number is float f)
            return float.ConvertToInteger<int>(f);
        else if (number is long l)
            return (int)l;
        else if (number is int i)
            return i;
        else if (number is short s)
            return s;
        else if (number is byte by)
            return by;
        else if (number is bool b)
            return b ? 1 : 0;
        else if (number is string str && int.TryParse(str, out int iStr))
            return iStr;

        return null;
    }
}
