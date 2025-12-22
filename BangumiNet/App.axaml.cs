using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace BangumiNet;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var window = new MainWindow { DataContext = new MainWindowViewModel() };
            desktop.MainWindow = window;
            desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;

            if (desktop.Args is { } args && args.Length >= 2)
                window.navigatorViewModel.Navigate(args);
        }

        Resources["ErrorBg"] = Brush.Parse(SettingProvider.Current.ColorErrorBg);
        Resources["OkBg"] = Brush.Parse(SettingProvider.Current.ColorOkBg);

        _ = BangumiDataProvider.LoadBangumiDataObject();

        CommonUtils.CleanUpTempFolder();

        if (SettingProvider.Current.CheckUpdateOnStartup)
            _ = SettingView.CheckUpdate(v => MessageWindow.Show(
                new TextViewModel(() => ["新版本可用\n", new HyperlinkButton { Content = v.ToString(), NavigateUri = new(Constants.SourceRepositoryLatestRelease) }]),
                "有新版本", FluentIcons.Common.Icon.ArrowCircleUp));

        //TextBlock.TextProperty.Changed.AddClassHandler<TextBlock>((tb, e) => tb.Text = System.Net.WebUtility.HtmlDecode(tb.Text));

        // 程序关闭时
        //((IClassicDesktopStyleApplicationLifetime?)Current?.ApplicationLifetime)?.ShutdownRequested += delegate (object? sender, ShutdownRequestedEventArgs e)
        //{
        //};

        base.OnFrameworkInitializationCompleted();
    }

    public override void RegisterServices()
    {
        base.RegisterServices();
        //AvaloniaWebViewBuilder.Initialize(config =>
        //{
        //    config.DefaultWebViewBackgroundColor = System.Drawing.Color.FromArgb(244, 244, 244);
        //    config.UserDataFolder = Path.Combine(SettingProvider.CurrentSettings.LocalDataDirectory, "WebView");
        //});
    }
}
