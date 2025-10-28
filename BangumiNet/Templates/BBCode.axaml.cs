using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using TheArtOfDev.HtmlRenderer.Avalonia;

namespace BangumiNet.Templates;

public class BBCode : TemplatedControl
{
    private ContentPresenter? contentPresenter;
    private readonly CompositeDisposable disposables = [];
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        contentPresenter = e.NameScope.Find<ContentPresenter>("PART_Text");
        disposables.Clear();

        this.WhenAnyValue(x => x.Text).Subscribe(text =>
        {
            if (BBCodeHelper.ContainsBBCode(text))
                contentPresenter?.Content = GetHtmlPanel(text);
            else
                contentPresenter?.Content = new SelectableTextBlock() { Text = text, TextWrapping = TextWrapping.Wrap };
        }).DisposeWith(disposables);
    }

    public static HtmlPanel GetHtmlPanel(string? text)
    {
        HtmlPanel hp = new();
        hp.ImageLoad += async (s, e) =>
        {
            if (e.Event.Handled) return;

            var src = e.Event.Src;
            if (src.StartsWith("http"))
                e.Event.Callback(await ApiC.GetImageAsync(e.Event.Src));
            else if (src.StartsWith("bn://emoji/") && int.TryParse(src[11..], out var emojiIndex))
                e.Event.Callback(StickerProvider.GetStickerBitmap(emojiIndex + 1));
            else if (src.StartsWith("bn://sticker/") && int.TryParse(src[13..], out var stickerIndex))
                e.Event.Callback(StickerProvider.GetStickerBitmap(stickerIndex + StickerProvider.Emojis.Length));

            e.Event.Handled = true;
            e.Handled = true;
        };
        hp.Text = BBCodeHelper.ParseBBCode(text);
        return hp;
    }

    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<BBCode, string?>(nameof(Text));
    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}