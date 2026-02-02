using Avalonia.Controls;
using Avalonia.Controls.Documents;
using BangumiNet.Common.BBCode;

namespace BangumiNet.Templates;

public class BBCode : TextBlock
{
    //public static async Task GetHtmlPanelAsync()
    //{
    //    var src = "link";
    //    if (src.StartsWith("http") || src.StartsWith("//"))
    //    {
    //        var bitmap = await ApiC.GetImageAsync(src);
    //    }
    //    else if (src.StartsWith("bn://emoji/") && int.TryParse(src[11..], out var emojiIndex))
    //        StickerProvider.GetStickerBitmap(emojiIndex + 1);
    //    else if (src.StartsWith("bn://sticker/") && int.TryParse(src[13..], out var stickerIndex))
    //        StickerProvider.GetStickerBitmap(stickerIndex + StickerProvider.Emojis.Length);
    //}

    public BBCode()
    {
        this.WhenAnyValue(x => x.Text).Subscribe(text =>
        {
            if (!string.IsNullOrWhiteSpace(text))
                Inlines = [.. BBCodeParser.Parse(text).SelectMany(GetBBControl)];
            else
                Inlines = null;
        });
    }

    public static Inline[] GetBBControl(BBNode bbnode)
    {
        if (bbnode is BBText text)
            return [new Run(text.Text)];

        if (bbnode is BBTag tag)
        {
            InlineCollection children = [.. tag.Children.SelectMany(GetBBControl)];

            if (tag.Name == "url")
            {
                var uri = Uri.TryCreate(tag.Attribute);
                uri ??= Uri.TryCreate(tag.ToPlainText());
                var link = new HyperlinkButton() { NavigateUri = uri, Content = new TextBlock { Inlines = children } };
                return [new InlineUIContainer(link)];
            }

            if (tag.Name == "img" && tag.Children is [BBText content])
            {
                var image = new MainImage { Url = content.Text };
                if (tag.Attribute?.Split(',') is [string w, string h])
                {
                    image.Width = double.TryParse(w) ?? double.NaN;
                    image.Height = double.TryParse(h) ?? double.NaN;
                }
                return [new LineBreak(), new InlineUIContainer(image), new LineBreak()];
            }

            if (tag.Name == "photo" && tag.Attribute is { } attr)
            {
                var image = new MainImage { Url = $"https://lain.bgm.tv/pic/photo/l/{attr}" };
                return [new LineBreak(), new InlineUIContainer(image), new LineBreak()];
            }

            return [new Span { Inlines = children }];
        }

        throw new NotImplementedException();
    }
}