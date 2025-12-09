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
    private readonly CompositeDisposable images = [];
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        contentPresenter = e.NameScope.Find<ContentPresenter>("PART_Text");
        disposables.Clear();

        this.WhenAnyValue(x => x.Text).Subscribe(text =>
        {
            if (string.IsNullOrWhiteSpace(text))
                contentPresenter?.Content = null;
            else if (BBCodeHelper.ContainsBBCode(text))
                contentPresenter?.Content = GetHtmlPanel(text, images);
            else
                contentPresenter?.Content = new SelectableTextBlock() { Text = text, TextWrapping = TextWrapping.Wrap };
        }).DisposeWith(disposables);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        images.Clear();
        disposables.Clear();
    }

    public static HtmlPanel GetHtmlPanel(string? text, CompositeDisposable disposables)
    {
        HtmlPanel hp = new();
        hp.ImageLoad += async (s, e) =>
        {
            if (e.Event.Handled) return;

            var src = e.Event.Src;
            if (src.StartsWith("http") || src.StartsWith("//"))
            {
                var bitmap = await ApiC.GetImageAsync(e.Event.Src, fallback: true);
                bitmap?.DisposeWith(disposables);
                e.Event.Callback(bitmap);
            }
            else if (src.StartsWith("bn://emoji/") && int.TryParse(src[11..], out var emojiIndex))
                e.Event.Callback(StickerProvider.GetStickerBitmap(emojiIndex + 1));
            else if (src.StartsWith("bn://sticker/") && int.TryParse(src[13..], out var stickerIndex))
                e.Event.Callback(StickerProvider.GetStickerBitmap(stickerIndex + StickerProvider.Emojis.Length));

            e.Event.Handled = true;
            e.Handled = true;
        };
        hp.LinkClicked += static async (s, e) =>
        {
            if (e.Event.Handled) return;

            var url = new Uri(e.Event.Link);
            if (url.Host != "bgm.tv" && url.Host != "bangumi.tv" && url.Host != "chii.in") return;
            e.Event.Handled = true;
            e.Handled = true;

            var path = url.AbsolutePath.Trim('/');
            int id;
            if (path.StartsWith("subject/topic/") && int.TryParse(path.Replace("subject/topic/", ""), out id))
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
                SecondaryWindow.Show(await ApiC.GetViewModelAsync<UserViewModel>(username: username));
            else if (path.StartsWith("group/") && path.Replace("group/", "") is string groupname && CommonUtils.IsAlphaNumeric(groupname))
                SecondaryWindow.Show(await ApiC.GetViewModelAsync<GroupViewModel>(username: groupname));
            else
            {
                e.Event.Handled = false;
                e.Handled = false;
            }
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