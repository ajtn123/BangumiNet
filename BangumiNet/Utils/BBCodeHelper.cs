using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class BBCodeHelper
{
    private const string ContentPos = "%CONTENT%";
    private const string SelectionBackgroundPos = "%SELECTIONBACKGROUND%";
    private const string HtmlFrame = $$"""
        <!DOCTYPE html>
        <html>
        <head>
        <style>
            html { margin: 0; padding: 0; }
            body { margin: 0; padding: 0; word-break: break-all; }
            span.mask { background-color: #000; color: #000; corner-radius: 5px; }
            blockquote { margin: 0.5em; color: #777; }
            ::selection { background-color: {{SelectionBackgroundPos}}; color: #000; }
        </style>
        </head>
        <body>{{ContentPos}}</body>
        </html>
        """;

    public static string ParseBBCode(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode)) return string.Empty;

        string result = bbcode.Trim(' ', '\n', '\r');
        foreach (var p in BBCodeReplacement) result = result.Replace(p.Key, p.Value);
        foreach (var p in BBCodeRegexReplacement) result = p.Key.Replace(result, p.Value);

        var html = HtmlFrame.Replace(ContentPos, result);

        object? brush = null;
        Application.Current?.TryGetResource("SystemFillColorAttentionBrush", out brush);
        var selectionBg = (brush as SolidColorBrush)?.Color.ToString().Replace("#ff", "#") ?? "#fff";
        html = html.Replace(SelectionBackgroundPos, selectionBg);

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
}
