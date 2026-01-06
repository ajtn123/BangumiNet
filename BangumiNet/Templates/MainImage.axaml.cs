using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using FluentIcons.Avalonia;
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
    public async Task LoadImageAsync()
    {
        images.Clear();

        if (string.IsNullOrWhiteSpace(Url))
            Source = null;
        else if (DefaultUserAvatarUrl().IsMatch(Url))
            Source = DefaultUserAvatar;
        else if (NoPhotoUrl().IsMatch(Url))
            Source = FallbackImage;
        else
        {
            var bitmap = await ApiC.GetImageAsync(Url);
            bitmap?.DisposeWith(images);
            Source = bitmap;
        }

        IsVisible = Source != null;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        this.WhenAnyValue(x => x.Url)
            .Subscribe(url => _ = LoadImageAsync())
            .DisposeWith(disposables);
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
        => await LoadImageAsync();

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var openBtn = e.NameScope.Find<Button>("PART_OpenImageButton");
        openBtn?.Click += OpenImageWithExternalProgram;
        var ReloadBtn = e.NameScope.Find<Button>("PART_ReloadImageButton");
        ReloadBtn?.Click += ReloadImage;
    }

    public static IImage DefaultUserAvatar { get; } = new Bitmap(AssetLoader.Open(CommonUtils.GetAssetUri("DefaultAvatar.png")));
    public static IImage FallbackImage { get; } = new FluentImage { Icon = FluentIcons.Common.Icon.Image };
    public static IImage InternetErrorFallbackImage { get; } = new FluentImage { Icon = FluentIcons.Common.Icon.GlobeError };

    [GeneratedRegex(@"^https?://lain\.bgm\.tv(/r/[0-9]+)?/pic/user/[A-Za-z]/icon\.jpg$")]
    private static partial Regex DefaultUserAvatarUrl();
    [GeneratedRegex(@"^https?://lain\.bgm\.tv/pic/photo/[A-Za-z]/no_photo\.png$")]
    private static partial Regex NoPhotoUrl();
}