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

    public static string ParseBBCode(string? bbcode)
    {
        if (string.IsNullOrEmpty(bbcode)) return string.Empty;

        string result = bbcode.Trim(' ', '\n');
        foreach (var p in BBCodeRegexReplacement) result = p.Key.Replace(result, p.Value);
        foreach (var p in BBCodeReplacement) result = result.Replace(p.Key, p.Value);

        return Html.Replace("{content}", result);
    }

    private static readonly string Html = """
        <!DOCTYPE html>
        <html>
        <head>
        <style>
            html { margin: 0; padding: 0; }
            body { margin: 0; padding: 0; }
            span.mask { background-color: #000; color: #000; corner-radius: 5px; }
            ::selection { background-color: %SelectionBackground%; color: #000; }
        </style>
        </head>
        <body>{content}</body>
        </html>
        """;
    private static readonly Dictionary<string, string> BBCodeReplacement = new()
    {
        ["[b]"] = "<b>",
        ["[/b]"] = "</b>",
        ["[i]"] = "<i>",
        ["[/i]"] = "</i>",
        ["[u]"] = "<u>",
        ["[/u]"] = "</u>",
        ["[s]"] = "<s>",
        ["[/s]"] = "</s>",
        ["[mask]"] = "<span class=\"mask\">",
        ["[/mask]"] = "</span>",
        ["[/color]"] = "</span>",
        ["[/size]"] = "</span>",
        ["[/url]"] = "</a>",
    };
    private static readonly Dictionary<Regex, string> BBCodeRegexReplacement = new()
    {
        [ColorOpen()] = "<span style=\"color: $1;\">",
        [SizeOpen()] = "<span style=\"font-size: $1px;\">",
        [LinkLiteral()] = "<a href=\"$1\">$1</a>",
        [LinkOpen()] = "<a href=\"$1\">",
        [Image()] = "<img src=\"$1\"></img>",
    };

    [GeneratedRegex(@"\[color=([#0-9a-zA-Z]*)\]")]
    private static partial Regex ColorOpen();
    [GeneratedRegex(@"\[size=([0-9]*)\]")]
    private static partial Regex SizeOpen();
    [GeneratedRegex(@"\[url]([^\[\]]*)\[/url\]")]
    private static partial Regex LinkLiteral();
    [GeneratedRegex(@"\[url=([^\[\]]*)\]")]
    private static partial Regex LinkOpen();
    [GeneratedRegex(@"\[img]([^\[\]]*)\[/img\]")]
    private static partial Regex Image();
}
