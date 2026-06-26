using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;
using System.Text.RegularExpressions;

namespace BangumiNet.Templates;

public partial class MainImage : ContentControl
{
    public static readonly StyledProperty<IImage?> SourceProperty
        = AvaloniaProperty.Register<MainImage, IImage?>(nameof(Source));
    public IImage? Source
    {
        get => GetValue(SourceProperty);
        set => SetValue(SourceProperty, value);
    }

    public static readonly StyledProperty<string?> UrlProperty
        = AvaloniaProperty.Register<MainImage, string?>(nameof(Url));
    public string? Url
    {
        get => GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }

    private readonly CompositeDisposable images = [];
    private readonly CompositeDisposable disposables = [];
    private async Task<IImage?> LoadImageAsync(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;
        else if (DefaultUserAvatarUrl().IsMatch(url))
            return DefaultUserAvatar;
        else if (NoPhotoUrl().IsMatch(url))
            return FallbackImage;
        else if (await ApiC.GetImageAsync(url) is { } bitmap)
        {
            bitmap.DisposeWith(images);
            return bitmap;
        }
        else
            return InternetErrorFallbackImage;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        this.WhenAnyValue(x => x.Url)
            .SelectMany(LoadImageAsync)
            .ObserveOn(RxSchedulers.MainThreadScheduler)
            .Subscribe(image =>
            {
                Source = image;
                IsVisible = image != null;
            }).DisposeWith(disposables);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        Source = null;
        disposables.Clear();
        images.Clear();
    }

    public void OpenImageWithExternalProgram(object? sender, RoutedEventArgs e)
    {
        if (Uri.TryCreate(Url, UriKind.Absolute, out var uri))
        {
            CommonUtils.OpenUri(uri);
        }
    }

    public async void ReloadImage(object? sender, RoutedEventArgs e)
        => Source = await LoadImageAsync(Url);
    public async void CopyUrl(object? sender, RoutedEventArgs e)
        => _ = TopLevel.GetTopLevel(this)?.Clipboard?.SetTextAsync(Url);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var openBtn = e.NameScope.Find<Button>("PART_OpenImageButton");
        openBtn?.Click += OpenImageWithExternalProgram;
        var ReloadBtn = e.NameScope.Find<Button>("PART_ReloadImageButton");
        ReloadBtn?.Click += ReloadImage;
        var CopyBtn = e.NameScope.Find<Button>("PART_CopyUrlButton");
        CopyBtn?.Click += CopyUrl;
    }

    public static IImage DefaultUserAvatar { get; } = new SvgImage()
    {
        Source = SvgSource.Load("DefaultAvatar.svg", CommonUtils.GetAssetUri("")),
        [!SvgImage.CssProperty] = App.Current.GetResourceObservable("TextFillColorPrimaryBrush").Select(color => $$"""path { color: {{(color as SolidColorBrush)?.Color.ToOpaqueString()}}; }""").ToBinding(),
    };
    public static IImage FallbackImage { get; } = IconHelper.GetFluentImage(FluentIcons.Common.Icon.Image);
    public static IImage InternetErrorFallbackImage { get; } = IconHelper.GetFluentImage(FluentIcons.Common.Icon.GlobeError);

    [GeneratedRegex(@"^https?://lain\.bgm\.tv(/r/[0-9]+)?/pic/user/[A-Za-z]/icon\.jpg$")]
    private static partial Regex DefaultUserAvatarUrl();
    [GeneratedRegex(@"^https?://lain\.bgm\.tv/pic/photo/[A-Za-z]/no_photo\.png$")]
    private static partial Regex NoPhotoUrl();
}
