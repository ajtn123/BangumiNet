using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
        else
        {
            var bitmap = await ApiC.GetImageAsync(url);
            bitmap?.DisposeWith(images);
            return bitmap;
        }
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        this.WhenAnyValue(x => x.Url)
            .SelectMany(LoadImageAsync)
            .ObserveOn(RxApp.MainThreadScheduler)
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
        if (Url is string url)
            if (CacheProvider.GetCacheFile(url) is string path)
            {
                var extension = url.Contains("png") ? "png"
                    : url.Contains("jpg") || url.Contains("jpeg") ? "jpg"
                    : "jpg";
                string tempFilePath = Path.Combine(PathProvider.TempFolderPath, Path.GetFileName(path) + "." + extension);

                Directory.CreateDirectory(PathProvider.TempFolderPath);
                if (!File.Exists(tempFilePath))
                    File.Copy(path, tempFilePath, true);
                CommonUtils.OpenUri(tempFilePath);
            }
            else
            {
                CommonUtils.OpenUri(url);
            }
    }

    public async void ReloadImage(object? sender, RoutedEventArgs e)
        => await LoadImageAsync(Url);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var openBtn = e.NameScope.Find<Button>("PART_OpenImageButton");
        openBtn?.Click += OpenImageWithExternalProgram;
        var ReloadBtn = e.NameScope.Find<Button>("PART_ReloadImageButton");
        ReloadBtn?.Click += ReloadImage;
    }

    public static IImage DefaultUserAvatar { get; } = new SvgImage()
    {
        Source = SvgSource.Load("DefaultAvatar.svg", CommonUtils.GetAssetUri("")),
        [!SvgImage.CssProperty] = App.Current!.GetResourceObservable("TextFillColorPrimaryBrush").Select(color => $$"""path { color: {{(color as SolidColorBrush)?.Color.ToOpaqueString()}}; }""").ToBinding(),
    };
    public static IImage FallbackImage { get; } = IconHelper.GetFluentImage(FluentIcons.Common.Icon.Image);
    public static IImage InternetErrorFallbackImage { get; } = IconHelper.GetFluentImage(FluentIcons.Common.Icon.GlobeError);

    [GeneratedRegex(@"^https?://lain\.bgm\.tv(/r/[0-9]+)?/pic/user/[A-Za-z]/icon\.jpg$")]
    private static partial Regex DefaultUserAvatarUrl();
    [GeneratedRegex(@"^https?://lain\.bgm\.tv/pic/photo/[A-Za-z]/no_photo\.png$")]
    private static partial Regex NoPhotoUrl();
}
