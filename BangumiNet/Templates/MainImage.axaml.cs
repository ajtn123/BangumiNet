using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.Templates;

public class MainImage : ContentControl
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

    private readonly CompositeDisposable disposables = [];
    public async Task LoadImageAsync()
    {
        var bitmap = await ApiC.GetImageAsync(Url);
        if (bitmap != null && !bitmap.IsShared())
            bitmap.DisposeWith(disposables);
        Source = bitmap;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _ = LoadImageAsync();
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        Source = null;
        disposables.Clear();
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
                Process.Start(new ProcessStartInfo(tempFilePath) { UseShellExecute = true });
            }
            else
            {
                CommonUtils.OpenUrlInBrowser(url);
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
}