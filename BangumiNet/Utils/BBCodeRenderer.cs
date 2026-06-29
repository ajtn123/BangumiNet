using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Documents;
using Avalonia.Media;
using Avalonia.Styling;
using BangumiNet.Common;
using System.Text.RegularExpressions;

namespace BangumiNet.Utils;

public static partial class BBCodeRenderer
{
    /// <summary>
    /// Parses BBCode and renders Avalonia controls.
    /// </summary>
    public static Control Render(string? bbcode)
    {
        if (string.IsNullOrWhiteSpace(bbcode))
            return new TextBlock { Text = "[空的 BBCode 段落]", Classes = { "Sec" } };

        var tokens = Tokenize(bbcode);
        var items = ParseTokens(tokens);
        return BuildPanel(items);
    }

    // Regex

    [GeneratedRegex(@"\[/?[biups]\]|\[/?(?:color|size|url|user|mask|quote|img|photo|code|center|right)(?:=[^\]]*)?\]|\[/?img(?:=\d+,\d+)?\]|\([a-z0-9_]+\)|\n", RegexOptions.IgnoreCase)]
    private static partial Regex TokenPattern();

    [GeneratedRegex(@"^img=(\d+),(\d+)$", RegexOptions.IgnoreCase)]
    private static partial Regex SizedImgTagPattern();

    [GeneratedRegex(@"^user=(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex UserTagPattern();

    [GeneratedRegex(@"^url=(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex UrlTagPattern();

    [GeneratedRegex(@"^color=(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex ColorTagPattern();

    [GeneratedRegex(@"^size=(.+)$", RegexOptions.IgnoreCase)]
    private static partial Regex SizeTagPattern();

    [GeneratedRegex(@"^\([a-z0-9_]+\)$")]
    private static partial Regex StickerPattern();

    // Tokenizer

    private static List<BbToken> Tokenize(string bbcode)
    {
        var tokens = new List<BbToken>();
        var matches = TokenPattern().Matches(bbcode);
        int lastIndex = 0;

        foreach (Match m in matches)
        {
            // Capture plain text before this token
            if (m.Index > lastIndex)
                tokens.Add(new BbToken(BbTokenType.Text, bbcode[lastIndex..m.Index]));

            var val = m.Value;
            if (val == "\n")
                tokens.Add(new BbToken(BbTokenType.Newline, val));
            else if (StickerPattern().IsMatch(val))
                tokens.Add(new BbToken(BbTokenType.Sticker, val));
            else if (val.StartsWith("[/"))
                tokens.Add(new BbToken(BbTokenType.CloseTag, val[1..^1].ToLowerInvariant()));
            else if (val.StartsWith('['))
                tokens.Add(new BbToken(BbTokenType.OpenTag, val[1..^1].ToLowerInvariant()));
            else
                tokens.Add(new BbToken(BbTokenType.Text, val));

            lastIndex = m.Index + m.Length;
        }

        // Capture trailing text
        if (lastIndex < bbcode.Length)
            tokens.Add(new BbToken(BbTokenType.Text, bbcode[lastIndex..]));

        return tokens;
    }

    // Parser

    private static List<PanelItem> ParseTokens(List<BbToken> tokens)
    {
        var items = new List<PanelItem>();
        var current = new List<Inline>();
        var stack = new Stack<FormatState>();

        void FlushParagraph()
        {
            if (current.Count > 0)
            {
                items.Add(new PanelItem.Paragraph(current));
                current = [];
            }
        }

        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];

            switch (token.Type)
            {
                case BbTokenType.Newline:
                    FlushParagraph();
                    break;

                case BbTokenType.OpenTag:
                    var tag = token.Value;
                    if (IsBlockTag(tag))
                    {
                        FlushParagraph();
                        var (block, skipTo) = ParseBlock(tag, tokens, i + 1);
                        if (block != null) items.Add(new PanelItem.Block(block));
                        i = skipTo;
                    }
                    else
                    {
                        var state = new FormatState();
                        ApplyOpenTag(tag, state);
                        stack.Push(state);
                    }
                    break;

                case BbTokenType.CloseTag:
                    if (stack.Count > 0) stack.Pop();
                    break;

                case BbTokenType.Sticker:
                    current.Add(MakeStickerInline(token.Value));
                    break;

                case BbTokenType.Text:
                    current.Add(MakeTextInline(token.Value, stack));
                    break;
            }
        }

        FlushParagraph();
        return items;
    }

    private static bool IsBlockTag(string tag)
    {
        return tag.StartsWith("img") || tag.StartsWith("photo") ||
               tag is "code" or "center" or "right";
    }

    private static (Control? block, int endIndex) ParseBlock(string tag, List<BbToken> tokens, int start)
    {
        var closeTag = tag.StartsWith("img") ? "/img" :
                       tag.StartsWith("photo") ? "/photo" :
                       $"/{tag}";

        // Collect inner text tokens
        var innerText = new System.Text.StringBuilder();
        int i = start;
        for (; i < tokens.Count; i++)
        {
            if (tokens[i].Type == BbTokenType.CloseTag && tokens[i].Value == closeTag)
                break;
            if (tokens[i].Type is BbTokenType.Text or BbTokenType.Newline or BbTokenType.Sticker)
                innerText.Append(tokens[i].Type == BbTokenType.Newline ? "\n" : tokens[i].Value);
        }

        var content = innerText.ToString().Trim();

        if (tag == "code")
            return (MakeCodeBlock(content), i);
        if (tag == "center")
            return (MakeAlignedBlock(content, TextAlignment.Center), i);
        if (tag == "right")
            return (MakeAlignedBlock(content, TextAlignment.Right), i);
        if (tag == "img")
            return (MakeImageBlock(content, null, null), i);
        if (SizedImgTagPattern().Match(tag) is { Success: true } sizeMatch)
            return (MakeImageBlock(content, int.Parse(sizeMatch.Groups[1].Value), int.Parse(sizeMatch.Groups[2].Value)), i);
        if (tag.StartsWith("photo"))
            return (MakePhotoBlock(content), i);

        return (null, i);
    }

    private static void ApplyOpenTag(string tag, FormatState state)
    {
        switch (tag)
        {
            case "b": state.Bold = true; break;
            case "i": state.Italic = true; break;
            case "u": state.Underline = true; break;
            case "s": state.Strikethrough = true; break;
            case "mask": state.Mask = true; break;
            case "quote": state.Quote = true; break;
            default:
                if (ColorTagPattern().Match(tag) is { Success: true } cm)
                    state.Color = TryParseColor(cm.Groups[1].Value);
                if (SizeTagPattern().Match(tag) is { Success: true } sm &&
                    double.TryParse(sm.Groups[1].Value, out var sz))
                    state.FontSize = sz;
                if (UrlTagPattern().Match(tag) is { Success: true } um)
                    state.Url = um.Groups[1].Value;
                if (UserTagPattern().Match(tag) is { Success: true } usm)
                    state.Url = $"{UrlProvider.BangumiTvUserUrlBase}{usm.Groups[1].Value}";
                if (tag == "url")
                    state.Url = ""; // literal URL — will be filled by inner text
                break;
        }
    }

    // Inline builders

    private static Inline MakeTextInline(string text, Stack<FormatState> stack)
    {
        // Apply formatting from the stack
        Run run = new(text);
        foreach (var state in stack)
        {
            if (state.Bold) run.FontWeight = FontWeight.Bold;
            if (state.Italic) run.FontStyle = FontStyle.Italic;
            if (state.Underline) run.TextDecorations = TextDecorations.Underline;
            if (state.Strikethrough) run.TextDecorations = TextDecorations.Strikethrough;
            if (state.Color is { } c) run.Foreground = new SolidColorBrush(c);
            if (state.FontSize is { } fs) run.FontSize = fs;
        }

        // Find the innermost URL or mask/quote state
        var urlState = stack.LastOrDefault(s => s.Url != null);
        var maskState = stack.LastOrDefault(s => s.Mask);
        var quoteState = stack.LastOrDefault(s => s.Quote);

        if (urlState?.Url is { } url)
        {
            var href = string.IsNullOrWhiteSpace(url) ? text : url;
            var button = new HyperlinkButton
            {
                Content = text,
                Foreground = Brushes.Blue,
                FontSize = run.FontSize,
                FontWeight = run.FontWeight,
                FontStyle = run.FontStyle,
            };
            button.Click += async (_, _) => await NavigateToBgmUrl(href);
            button.SetValue(ToolTip.TipProperty, href);
            return new InlineUIContainer(button) { BaselineAlignment = BaselineAlignment.Bottom };
        }

        if (maskState != null)
        {
            var maskText = new TextBlock { Inlines = [run] };

            return new InlineUIContainer(maskText)
            {
                BaselineAlignment = BaselineAlignment.Bottom,
                Styles =
                {
                    new Style(x => x.OfType<TextBlock>())
                    {
                        Setters =
                        {
                            new Setter(TextBlock.ForegroundProperty, Brushes.Black),
                            new Setter(TextBlock.BackgroundProperty, Brushes.Black),
                        }
                    },
                    new Style(x => x.OfType<TextBlock>().Class(":pointerover"))
                    {
                        Setters =
                        {
                            new Setter(TextBlock.BackgroundProperty, Brushes.Transparent),
                        }
                    }
                }
            };
        }

        if (quoteState != null)
        {
            run.Foreground = Brushes.Gray;
            return run;
        }

        return run;
    }

    private static Inline MakeStickerInline(string stickerText)
    {
        if (StickerService.GetUrlByCode(stickerText) is not { } url)
            return new Run { Text = stickerText, Classes = { "Sec" } };
        var bitmap = StickerProvider.GetStickerByUrl(url);
        var image = new DelayedImage(bitmap) { Width = 16, Height = 16 };
        return new InlineUIContainer(image);
    }

    // Block builders

    private static Control MakeCodeBlock(string content)
    {
        return new Border
        {
            Background = new SolidColorBrush(Color.Parse("#1E1E1E")),
            CornerRadius = new CornerRadius(6),
            Padding = new Thickness(10),
            Child = new SelectableTextBlock
            {
                Text = content,
                FontFamily = new FontFamily("Consolas, Courier New, monospace"),
                TextWrapping = TextWrapping.Wrap,
            }
        };
    }

    private static Control MakeAlignedBlock(string content, TextAlignment alignment)
    {
        return new TextBlock
        {
            Text = content,
            TextAlignment = alignment,
            TextWrapping = TextWrapping.Wrap,
        };
    }

    private static Control MakeImageBlock(string src, int? width, int? height)
    {
        var fullSrc = src.StartsWith("//") ? $"https:{src}" : src;
        return new MainImage
        {
            Url = fullSrc,
            Width = width ?? double.NaN,
            Height = height ?? double.NaN,
        };
    }

    private static Control MakePhotoBlock(string path)
    {
        return new MainImage
        {
            Url = $"https://lain.bgm.tv/pic/photo/l/{path}",
        };
    }

    // ── Panel builder ───────────────────────────────────────────────────

    private static Control BuildPanel(List<PanelItem> items)
    {
        var panel = new StackPanel();

        foreach (var item in items)
        {
            switch (item)
            {
                case PanelItem.Paragraph p:
                    if (p.Inlines.Count == 0) continue;
                    var tb = new TextBlock
                    {
                        TextWrapping = TextWrapping.Wrap,
                        Inlines = [],
                    };
                    foreach (var inline in p.Inlines)
                        tb.Inlines.Add(inline);
                    panel.Children.Add(tb);
                    break;

                case PanelItem.Block b:
                    panel.Children.Add(b.Control);
                    break;
            }
        }

        return panel;
    }

    // Helpers

    private static Color? TryParseColor(string value) => Color.TryParse(value, out var c) ? c : null;

    private static async Task NavigateToBgmUrl(string url)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return;

        if (uri.Host != "bgm.tv" && uri.Host != "bangumi.tv" && uri.Host != "chii.in")
            CommonUtils.OpenUri(uri);

        var path = uri.AbsolutePath.Trim('/');

        if (path.StartsWith("subject/topic/") && int.TryParse(path.Replace("subject/topic/", ""), out int id))
            SecondaryWindow.Show(await ApiC.GetTopicViewModelAsync(ItemType.Subject, id));
        else if (path.StartsWith("group/topic/") && int.TryParse(path.Replace("group/topic/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetTopicViewModelAsync(ItemType.Group, id));
        else if (path.StartsWith("character/") && int.TryParse(path.Replace("character/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<CharacterViewModel>(id));
        else if (path.StartsWith("subject/") && int.TryParse(path.Replace("subject/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<SubjectViewModel>(id));
        else if (path.StartsWith("person/") && int.TryParse(path.Replace("person/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<PersonViewModel>(id));
        else if (path.StartsWith("blog/") && int.TryParse(path.Replace("blog/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<BlogViewModel>(id));
        else if (path.StartsWith("index/") && int.TryParse(path.Replace("index/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<IndexViewModel>(id));
        else if (path.StartsWith("ep/") && int.TryParse(path.Replace("ep/", ""), out id))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<EpisodeViewModel>(id));
        else if (path.StartsWith("user/") && path.Replace("user/", "") is string username && CommonUtils.IsAlphaNumeric(username))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<UserViewModel>(name: username));
        else if (path.StartsWith("group/") && path.Replace("group/", "") is string groupname && CommonUtils.IsAlphaNumeric(groupname))
            SecondaryWindow.Show(await ApiC.GetViewModelAsync<GroupViewModel>(name: groupname));
        else CommonUtils.OpenUri(uri);
    }

    // Types

    private enum BbTokenType { Text, OpenTag, CloseTag, Newline, Sticker }

    private record BbToken(BbTokenType Type, string Value);

    private abstract record PanelItem
    {
        public sealed record Paragraph(List<Inline> Inlines) : PanelItem;
        public sealed record Block(Control Control) : PanelItem;
    }

    private class FormatState
    {
        public bool Bold;
        public bool Italic;
        public bool Underline;
        public bool Strikethrough;
        public bool Mask;
        public bool Quote;
        public Color? Color;
        public double? FontSize;
        public string? Url;
    }
}
