using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Media;
using System.IO;

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

    public void OpenImageWithExternalProgram(object? sender, RoutedEventArgs e)
    {
        if (Tag is string url && !string.IsNullOrWhiteSpace(url))
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
                Common.OpenUrlInBrowser(url);
            }
    }

    public async void ReloadImage(object? sender, RoutedEventArgs e)
    {
        if (Tag is string url && !string.IsNullOrWhiteSpace(url))
        {
            CacheProvider.DeleteCache(url);
            Source = await ApiC.GetImageAsync(url);
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        var openBtn = e.NameScope.Find<Button>("PART_OpenImageButton");
        openBtn?.Click += OpenImageWithExternalProgram;
        var ReloadBtn = e.NameScope.Find<Button>("PART_ReloadImageButton");
        ReloadBtn?.Click += ReloadImage;
    }
}