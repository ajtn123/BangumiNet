using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Text.RegularExpressions;
using TheArtOfDev.HtmlRenderer.Avalonia;

namespace BangumiNet.Utils;

public static partial class BBCodeHelper
{
    public const string ContentPos = "%CONTENT%";
    public const string SelectionBackgroundPos = "%SELECTIONBACKGROUND%";
    public static void Parser(HtmlPanel hp, AvaloniaPropertyChangedEventArgs e)
    {
        var bbcode = hp.Tag?.ToString();
        if (string.IsNullOrWhiteSpace(bbcode)) return;

        var html = ParseBBCode(bbcode);
        object? brush = null;
        Application.Current?.TryGetResource("SystemFillColorAttentionBrush", out brush);
        html = html.Replace(SelectionBackgroundPos, (brush as SolidColorBrush)?.Color.ToString().Replace("#ff", "#") ?? "#fff");
        hp.Text = html;
    }

    public static string ParseBBCode(string? bbcode)
    {
        if (string.IsNullOrEmpty(bbcode)) return string.Empty;

        string result = bbcode.Trim(' ', '\n', '\r');
        foreach (var p in BBCodeRegexReplacement) result = p.Key.Replace(result, p.Value);
        foreach (var p in BBCodeReplacement) result = result.Replace(p.Key, p.Value);

        return Html.Replace(ContentPos, result);
    }

    private static readonly string Html = $$"""
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
    private static readonly Dictionary<string, string> BBCodeReplacement = new()
    {
        ["\r\n"] = "<br/>",
        ["\n"] = "<br/>",
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
        ["<br/>[quote]"] = "[quote]",
        ["[/quote]<br/>"] = "[/quote]",
        ["[quote]"] = "<blockquote>",
        ["[/quote]"] = "</blockquote>",
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
        [Image()] = "<img src=\"$1\"/>",
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
