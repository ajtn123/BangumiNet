using System.Net;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class CommonUtils
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

    public static DateTimeOffset? ParseBangumiTime(int? time)
    {
        if (time is not int t) return null;
        return DateTimeOffset.FromUnixTimeSeconds(t).ToLocalTime();
    }

    public static void OpenUrlInBrowser(string url)
        => Process.Start(new ProcessStartInfo()
        {
            FileName = url,
            UseShellExecute = true,
        });

    public static void SearchWeb(string? keyword, string? searchEngine = null)
    {
        if (string.IsNullOrEmpty(keyword)) return;
        searchEngine ??= SettingProvider.Current.DefaultSearchEngine;
        if (!SettingProvider.Current.SearchQueryUrlBases.TryGetValue(searchEngine, out var ub)) return;
        OpenUrlInBrowser(ub + WebUtility.UrlEncode(keyword));
    }

    public static bool IsAlphaNumeric(string? input)
        => AlphaNumeric().IsMatch(input ?? string.Empty);

    [GeneratedRegex(@"^[a-zA-Z0-9_]+$")]
    public static partial Regex AlphaNumeric();

    public static void CleanUpTempFolder()
    {
        if (!Path.Exists(PathProvider.TempFolderPath)) return;
        foreach (var file in Directory.EnumerateFiles(PathProvider.TempFolderPath))
            try { File.Delete(file); }
            catch (Exception e) { Trace.TraceWarning(e.Message); }
    }

    public static Uri GetAssetUri(string path)
        => new($"avares://BangumiNet/Assets/{path}");

    public static double RandomDouble(double min, double max)
    {
        if (min > max) throw new ArgumentException("min is greater than max");
        if (min == max) return min;

        var range = max - min;
        var offset = range * Random.Shared.NextDouble();
        return min + offset;
    }

    public static int? ToInt32(object? obj)
    {
        if (obj == null) return null;
        try
        {
            return Convert.ToInt32(obj);
        }
        catch { return null; }
    }
}
