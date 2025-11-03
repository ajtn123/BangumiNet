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
        <style>
            html { margin: 0; padding: 0; }
            body { margin: 0; padding: 0; {{ThemeTextColorPos}} }
            span.mask { background-color: #000; color: #000; corner-radius: 5px; }
            blockquote { margin: 0.5em; color: #777; }
            ::selection { background-color: {{SelectionBackgroundPos}}; {{ThemeTextColorPos}} }
            img { max-width: 98%; }
        </style>
        </head>
        <body>{{ContentPos}}</body>
        </html>
        """;

    private static string GetThemeTextColor() => $"color: {(Application.Current?.ActualThemeVariant.Key.ToString() == "Dark" ? "#fff" : "#000")};";
    public static string ParseBBCode(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode)) return string.Empty;

        string result = WebUtility.HtmlEncode(bbcode.Trim(' ', '\n', '\r'));
        foreach (var p in BBCodeReplacement) result = result.Replace(p.Key, p.Value);
        foreach (var p in BBCodeRegexReplacement) result = p.Key.Replace(result, p.Value);
        foreach (var (i, s) in StickerProvider.Emojis.Index()) result = result.Replace(s, $"<img src=\"bn://emoji/{i}\">");

        var html = HtmlFrame.Replace(ContentPos, result);

        object? brush = null;
        Application.Current?.TryGetResource("SystemFillColorAttentionBrush", out brush);
        var selectionBg = (brush as SolidColorBrush)?.Color.ToOpaqueString().ToLower() ?? "#fff";
        html = html.Replace(SelectionBackgroundPos, selectionBg);
        html = html.Replace(ThemeTextColorPos, GetThemeTextColor());

        return html;
    }

    public static bool ContainsBBCode(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode)) return false;

        if (BBCodeReplacement
            .Where(p => p.Value != HtmlNewLine)
            .Any(p => bbcode.Contains(p.Key))) return true;
        if (BBCodeRegexReplacement
            .Any(r => r.Key.IsMatch(bbcode))) return true;
        if (StickerProvider.Emojis
            .Any(bbcode.Contains)) return true;

        return false;
    }

    private const string HtmlNewLine = "<br/>";
    private static readonly Dictionary<string, string> BBCodeReplacement = new()
    {
        ["\r\n"] = HtmlNewLine,
        ["\r"] = HtmlNewLine,
        ["\n"] = HtmlNewLine,
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
        [Image()] = "<img src=\"$1\"/>",
        [Quote()] = "<blockquote>$2</blockquote>",
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
    [GeneratedRegex(@"\[img\](.*?)\[/img\]")]
    private static partial Regex Image();
    [GeneratedRegex(@"(<br/>)? *\[quote\](.*?)\[/quote\] *(<br/>)?")]
    private static partial Regex Quote();
    [GeneratedRegex(@"\(bgm([0-9]{1,3})\)")]
    private static partial Regex Sticker();
}
