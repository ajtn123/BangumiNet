using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using BangumiNet.ViewModels;
using BangumiNet.Views;
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

        TextBlock.TextProperty.Changed.AddClassHandler<TextBlock>((tb, e) => tb.Text = WebUtility.HtmlDecode(tb.Text));

        base.OnFrameworkInitializationCompleted();
    }
}
