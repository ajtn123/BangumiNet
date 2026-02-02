using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Styling;
using BangumiNet.Library;
using FluentAvalonia.Styling;

namespace BangumiNet;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        ApplySettings(SettingProvider.Current);

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

        EventHandlers.AddHandlers();

        // 程序关闭时
        (Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime)?.ShutdownRequested += (s, e) =>
        {
            LibrarySubjectProvider.Save();
        };

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

    public void ApplySettings(Settings settings)
    {
        UpdateThemeSettings(settings);
        Shared.Utils.SetupLogger(settings);
    }

    private void UpdateThemeSettings(Settings settings)
    {
        if (Styles[0] is not FluentAvaloniaTheme theme) return;

        theme.PreferSystemTheme = settings.ApplicationTheme == ApplicationTheme.System;
        RequestedThemeVariant = settings.ApplicationTheme switch
        {
            ApplicationTheme.Light => ThemeVariant.Light,
            ApplicationTheme.Dark => ThemeVariant.Dark,
            ApplicationTheme.System => ThemeVariant.Default,
            _ => throw new NotImplementedException(),
        };

        theme.PreferUserAccentColor = settings.UseSystemAccentColor;
        if (settings.UseSystemAccentColor)
            theme.CustomAccentColor = null;
        else if (Color.TryParse(settings.ColorCustomAccent, out var color))
            theme.CustomAccentColor = color;
    }

    public static new App Current => (App)Application.Current!;
}
