using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System.Net;

namespace BangumiNet;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel() };

        Resources["ErrorBg"] = Brush.Parse(SettingProvider.CurrentSettings.ErrorBg);
        Resources["OkBg"] = Brush.Parse(SettingProvider.CurrentSettings.OkBg);

        CacheProvider.CalculateCacheSize();

        Common.CleanUpTempFolder();

        TextBlock.TextProperty.Changed.AddClassHandler<TextBlock>((tb, e) => tb.Text = WebUtility.HtmlDecode(tb.Text));

        // 程序关闭时
        //((IClassicDesktopStyleApplicationLifetime?)Current?.ApplicationLifetime)?.ShutdownRequested += delegate (object? sender, ShutdownRequestedEventArgs e)
        //{
        //};

        base.OnFrameworkInitializationCompleted();
    }
}
