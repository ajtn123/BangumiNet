using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Net;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class BBCodeHelper
{
    private const string ContentPos = "%CONTENT%";
    private const string SelectionBackgroundPos = "%SELECTIONBACKGROUND%";
    private const string ThemeTextColorPos = "%THEMETEXTCOLOR%";
    private const string HtmlFrame = $$"""
        <!DOCTYPE html>
        <html>
        <head>
        <meta charset="UTF-8">
        <style>
            html, body, p { margin: 0; padding: 0; }
            body { {{ThemeTextColorPos}} }
            p { word-break: break-all; white-space: pre-wrap; }
            span.mask { background-color: #000; color: #000; corner-radius: 5px; }
            span.quote { border-left: 1px solid #777; color: #777; }
            ::selection { {{SelectionBackgroundPos}} {{ThemeTextColorPos}} }
            img { max-width: 98%; }
        </style>
        </head>
        <body><p>{{ContentPos}}</p></body>
        </html>
        """;

    private static string GetThemeTextColor() => $"color: {(Application.Current?.ActualThemeVariant.Key.ToString() == "Dark" ? "#fff" : "#000")};";
    private static string GetAccentBg()
    {
        object? brush = null;
        Application.Current?.TryGetResource("SystemFillColorAttentionBrush", out brush);
        var color = (brush as SolidColorBrush)?.Color.ToOpaqueString();
        if (color == null) return string.Empty;
        return $"background-color: {color};";
    }
    public static string ParseBBCode(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode)) return string.Empty;

        var result = WebUtility.HtmlEncode(bbcode)
            .Trim('\n', '\r')
            .ReplaceLineEndings("</p><p>")
            .Replace("<p></p>", "<br/>");
        foreach (var p in BBCodeReplacement) result = result.Replace(p.Key, p.Value);
        foreach (var p in BBCodeRegexReplacement) result = p.Key.Replace(result, p.Value);
        foreach (var (i, s) in StickerProvider.Emojis.Index()) result = result.Replace(s, $"<img src=\"bn://emoji/{i}\">");

        var html = HtmlFrame.Replace(ContentPos, result)
            .Replace(SelectionBackgroundPos, GetAccentBg())
            .Replace(ThemeTextColorPos, GetThemeTextColor());

        return html;
    }

    public static bool ContainsBBCode(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode)) return false;

        if (BBCodeReplacement
            .Any(p => bbcode.Contains(p.Key))) return true;
        if (BBCodeRegexReplacement
            .Any(r => r.Key.IsMatch(bbcode))) return true;
        if (StickerProvider.Emojis
            .Any(bbcode.Contains)) return true;

        return false;
    }

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
        ["[center]"] = "<div align=\"center\">",
        ["[/center]"] = "</div>",
        ["[right]"] = "<div align=\"right\">",
        ["[/right]"] = "</div>",
        ["[mask]"] = "<span class=\"mask\">",
        ["[/mask]"] = "</span>",
        ["[/color]"] = "</span>",
        ["[/size]"] = "</span>",
    };
    private static readonly Dictionary<Regex, string> BBCodeRegexReplacement = new()
    {
        [ColorOpen()] = "<span style=\"color: $1;\">",
        [SizeOpen()] = "<span style=\"font-size: $1px;\">",
        [LinkLiteral()] = "<a href=\"$1\">$1</a>",
        [LinkCovered()] = "<a href=\"$1\">$2</a>",
        [User()] = $"<a href=\"{UrlProvider.BangumiTvUserUrlBase}$1\">@$2</a>",
        [Image()] = "<img src=\"$1\"/>",
        [ImageSized()] = "<img src=\"$3\" width=\"$1\" height=\"$2\"/>",
        [Quote()] = "<span class=\"quote\"> $1</span>",
        [Sticker()] = "<img src=\"bn://sticker/$1\"/>",
    };

    [GeneratedRegex(@"\[color=([#0-9a-zA-Z]*)\]")]
    private static partial Regex ColorOpen();
    [GeneratedRegex(@"\[size=([\.0-9a-zA-Z]*)\]")]
    private static partial Regex SizeOpen();
    [GeneratedRegex(@"\[url\](.*?)\[/url\]")]
    private static partial Regex LinkLiteral();
    [GeneratedRegex(@"\[url=(.*?)\](.*?)\[/url\]")]
    private static partial Regex LinkCovered();
    [GeneratedRegex(@"\[user=(.*?)\](.*?)\[/user\]")]
    private static partial Regex User();
    [GeneratedRegex(@"\[img\](.*?)\[/img\]")]
    private static partial Regex Image();
    [GeneratedRegex(@"\[img=([0-9]+),([0-9]+)\](.*?)\[/img\]")]
    private static partial Regex ImageSized();
    [GeneratedRegex(@" *\[quote\](.*?)\[/quote\] *")]
    private static partial Regex Quote();
    [GeneratedRegex(@"\(bgm([0-9]{1,3})\)")]
    private static partial Regex Sticker();
}
