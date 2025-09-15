using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BangumiNet.Converters;
using BangumiNet.Shared;
using BangumiNet.ViewModels;
using BangumiNet.Views;

namespace BangumiNet;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel() };
            desktop.Exit += (s, e) => { CacheProvider.SaveCacheList(); };
        }

        Resources["NameCnConverter"] = NameCnConverter.Instance;

        CacheProvider.LoadCacheList();

        base.OnFrameworkInitializationCompleted();
    }
}