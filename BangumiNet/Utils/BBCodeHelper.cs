using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System.Text.RegularExpressions;
using TheArtOfDev.HtmlRenderer.Avalonia;

namespace BangumiNet.Utils;

public static partial class BBCodeHelper
{
    public static void Parser(HtmlPanel hp, AvaloniaPropertyChangedEventArgs e)
    {
        var html = ParseBBCode(hp.Tag?.ToString());
        object? brush = null;
        Application.Current?.TryGetResource("SystemFillColorAttentionBrush", out brush);
        html = html.Replace("%SelectionBackground%", (brush as SolidColorBrush)?.Color.ToString().Replace("#ff", "#") ?? "#fff");
        hp.Text = html;
    }

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
            body { margin: 0; padding: 0; word-break: break-all; }
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
