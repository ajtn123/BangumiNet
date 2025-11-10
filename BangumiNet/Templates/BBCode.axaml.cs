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
            if (string.IsNullOrWhiteSpace(text))
                contentPresenter?.Content = null;
            else if (BBCodeHelper.ContainsBBCode(text))
                contentPresenter?.Content = GetHtmlPanel(text);
            else
                contentPresenter?.Content = new SelectableTextBlock() { Text = text, TextWrapping = TextWrapping.Wrap };
        }).DisposeWith(disposables);
    }

    public static HtmlPanel GetHtmlPanel(string? text)
    {
        HtmlPanel hp = new();
        hp.ImageLoad += static async (s, e) =>
        {
            if (e.Event.Handled) return;

            var src = e.Event.Src;
            if (src.StartsWith("http"))
                e.Event.Callback(await ApiC.GetImageAsync(e.Event.Src, fallback: true));
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
            if (path.StartsWith("subject/topic/") && int.TryParse(path.Replace("subject/topic/", ""), out var stid))
                new SecondaryWindow() { Content = await ApiC.GetTopicViewModelAsync(ItemType.Subject, stid) }.Show();
            else if (path.StartsWith("group/topic/") && int.TryParse(path.Replace("group/topic/", ""), out var gtid))
                new SecondaryWindow() { Content = await ApiC.GetTopicViewModelAsync(ItemType.Group, gtid) }.Show();
            else if (path.StartsWith("character/") && int.TryParse(path.Replace("character/", ""), out var cid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<CharacterViewModel>(cid) }.Show();
            else if (path.StartsWith("subject/") && int.TryParse(path.Replace("subject/", ""), out var sid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<SubjectViewModel>(sid) }.Show();
            else if (path.StartsWith("person/") && int.TryParse(path.Replace("person/", ""), out var pid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<PersonViewModel>(pid) }.Show();
            else if (path.StartsWith("ep/") && int.TryParse(path.Replace("ep/", ""), out var eid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<EpisodeViewModel>(eid) }.Show();
            else if (path.StartsWith("user/") && path.Replace("user/", "") is string uid && Common.IsAlphaNumeric(uid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<UserViewModel>(username: uid) }.Show();
            else if (path.StartsWith("group/") && path.Replace("group/", "") is string gid && Common.IsAlphaNumeric(gid))
                new SecondaryWindow() { Content = await ApiC.GetViewModelAsync<GroupViewModel>(username: gid) }.Show();
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