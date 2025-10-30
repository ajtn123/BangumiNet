using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class Common
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
        searchEngine ??= SettingProvider.CurrentSettings.DefaultSearchEngine;
        if (!SettingProvider.CurrentSettings.SearchQueryUrlBases.TryGetValue(searchEngine, out var ub)) return;
        OpenUrlInBrowser(ub + WebUtility.UrlEncode(keyword));
    }

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
}
