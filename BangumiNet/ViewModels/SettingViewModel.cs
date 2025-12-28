using Avalonia;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reactive;
using System.Reflection;

namespace BangumiNet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel(Settings settings)
    {
        DefaultSettings = new();
        Source = settings;
        Overrides = settings.GetOverrides();

        Title = $"设置 - {Title}";
        SearchEngineSuggestions = [.. settings.SearchQueryUrlBases.Keys];

        UserAgent = GetOverride(x => x.UserAgent) ?? "";
        AuthToken = settings.AuthToken;
        BangumiTvUrlBase = GetOverride(x => x.BangumiTvUrlBase) ?? "";
        DefaultSearchEngine = settings.DefaultSearchEngine;
        LocalDataDirectory = GetOverride(x => x.LocalDataDirectory) ?? "";
        LibraryDirectories = settings.LibraryDirectories;
        IsDiskCacheEnabled = settings.IsDiskCacheEnabled;
        DiskCacheSizeLimit = GetOverrideValue(x => x.DiskCacheSizeLimit);
        CollectionPageSize = GetOverrideValue(x => x.CollectionPageSize);
        RevisionPageSize = GetOverrideValue(x => x.RevisionPageSize);
        CommentPageSize = GetOverrideValue(x => x.CommentPageSize);
        SearchPageSize = GetOverrideValue(x => x.SearchPageSize);
        EpisodePageSize = GetOverrideValue(x => x.EpisodePageSize);
        SubjectBrowserPageSize = GetOverrideValue(x => x.SubjectBrowserPageSize);
        PreferChineseNames = settings.PreferChineseNames;
        CheckUpdateOnStartup = settings.CheckUpdateOnStartup;
        SaveLogFile = settings.SaveLogFile;
        ShowSplashScreenOnAppStartup = settings.ShowSplashScreenOnAppStartup;
        ShowSplashScreenOnWindowStartup = settings.ShowSplashScreenOnWindowStartup;
        ApplicationTheme = settings.ApplicationTheme;
        UseSystemAccentColor = settings.UseSystemAccentColor;

        Palette = [.. PaletteItemViewModel.GetPalette(settings)];

        UndoChangesCommand = ReactiveCommand.Create(() => { });
        RestoreCommand = ReactiveCommand.Create(() => { });
        GetTokenCommand = ReactiveCommand.Create(() => CommonUtils.OpenUri(Constants.BangumiTokenManagerUrl));
        SaveCommand = ReactiveCommand.Create(() =>
        {
            var newSettings = ToSettings();
            SettingProvider.UpdateSettings(newSettings);
            ((App)Application.Current!).UpdateThemeSettings(newSettings);
            if (newSettings.AuthToken != Source.AuthToken || newSettings.UserAgent != Source.UserAgent)
            {
                ApiC.RebuildClients();
                var mainWindow = MainWindow.Instance;
                mainWindow.meVM = null;
                mainWindow.homeVM = null;
            }
        });
        DumpCacheCommand = ReactiveCommand.Create(() =>
        {
            CacheProvider.DumpCache();
            this.RaisePropertyChanged(nameof(CacheSizeString));
        });
    }

    public Settings ToSettings()
    {
        var newSettings = new Settings()
        {
            UserAgent = GetValue(UserAgent, DefaultSettings.UserAgent),
            AuthToken = AuthToken,
            BangumiTvUrlBase = GetValue(BangumiTvUrlBase, DefaultSettings.BangumiTvUrlBase),
            DefaultSearchEngine = DefaultSearchEngine,
            LocalDataDirectory = GetValue(LocalDataDirectory, DefaultSettings.LocalDataDirectory),
            LibraryDirectories = GetValue(LibraryDirectories),
            IsDiskCacheEnabled = IsDiskCacheEnabled,
            DiskCacheSizeLimit = DiskCacheSizeLimit ?? DefaultSettings.DiskCacheSizeLimit,
            CollectionPageSize = CollectionPageSize ?? DefaultSettings.CollectionPageSize,
            RevisionPageSize = RevisionPageSize ?? DefaultSettings.RevisionPageSize,
            CommentPageSize = CommentPageSize ?? DefaultSettings.CommentPageSize,
            SearchPageSize = SearchPageSize ?? DefaultSettings.SearchPageSize,
            EpisodePageSize = EpisodePageSize ?? DefaultSettings.EpisodePageSize,
            SubjectBrowserPageSize = SubjectBrowserPageSize ?? DefaultSettings.SubjectBrowserPageSize,
            PreferChineseNames = PreferChineseNames,
            CheckUpdateOnStartup = CheckUpdateOnStartup,
            SaveLogFile = SaveLogFile,
            ShowSplashScreenOnAppStartup = ShowSplashScreenOnAppStartup,
            ShowSplashScreenOnWindowStartup = ShowSplashScreenOnWindowStartup,
            ApplicationTheme = ApplicationTheme,
            UseSystemAccentColor = UseSystemAccentColor,
            SearchQueryUrlBases = Source.SearchQueryUrlBases,
        };

        foreach (var item in Palette)
            item.Set(newSettings);

        return newSettings;
    }

    public static string GetValue(string? value, string defaultValue)
        => string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    public static string? GetValue(string? value)
        => string.IsNullOrWhiteSpace(value) ? null : value;
    private T? GetOverride<T>(Expression<Func<Settings, T>> selector) where T : class
    {
        var name = ((MemberExpression)selector.Body).Member.Name;
        return Overrides.TryGet(name) as T;
    }

    private T? GetOverrideValue<T>(Expression<Func<Settings, T>> selector) where T : struct
    {
        var name = ((MemberExpression)selector.Body).Member.Name;
        return (T?)Overrides.TryGet(name);
    }

    [Reactive] public partial Settings Source { get; set; }
    [Reactive] public partial Dictionary<string, object?> Overrides { get; set; }
    [Reactive] public partial Settings DefaultSettings { get; set; }
    [Reactive] public partial string UserAgent { get; set; }
    [Reactive] public partial string? AuthToken { get; set; }
    [Reactive] public partial string BangumiTvUrlBase { get; set; }
    [Reactive] public partial string DefaultSearchEngine { get; set; }
    [Reactive] public partial string LocalDataDirectory { get; set; }
    [Reactive] public partial string? LibraryDirectories { get; set; }
    [Reactive] public partial bool IsDiskCacheEnabled { get; set; }
    [Reactive] public partial long? DiskCacheSizeLimit { get; set; }
    [Reactive] public partial int? CollectionPageSize { get; set; }
    [Reactive] public partial int? RevisionPageSize { get; set; }
    [Reactive] public partial int? CommentPageSize { get; set; }
    [Reactive] public partial int? SearchPageSize { get; set; }
    [Reactive] public partial int? EpisodePageSize { get; set; }
    [Reactive] public partial int? SubjectBrowserPageSize { get; set; }
    [Reactive] public partial bool PreferChineseNames { get; set; }
    [Reactive] public partial bool CheckUpdateOnStartup { get; set; }
    [Reactive] public partial bool SaveLogFile { get; set; }
    [Reactive] public partial bool ShowSplashScreenOnAppStartup { get; set; }
    [Reactive] public partial bool ShowSplashScreenOnWindowStartup { get; set; }
    [Reactive] public partial ApplicationTheme ApplicationTheme { get; set; }
    [Reactive] public partial bool UseSystemAccentColor { get; set; }

    [Reactive] public partial List<string> SearchEngineSuggestions { get; set; }
    [Reactive] public partial List<PaletteItemViewModel> Palette { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> UndoChangesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> RestoreCommand { get; set; }
    public ReactiveCommand<Unit, Unit> GetTokenCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DumpCacheCommand { get; set; }

    public static string CacheSizeString => $"{CacheProvider.CacheSize} /";
}

public partial class PaletteItemViewModel : ViewModelBase
{
    public static IEnumerable<PaletteItemViewModel> GetPalette(Settings settings) => settings
        .GetType().GetProperties()
        .Where(prop => prop.Name.StartsWith("Color"))
        .Select(prop =>
        {
            var item = new PaletteItemViewModel
            {
                Color = (string)prop.GetValue(settings)!,
                Name = prop.GetCustomAttribute<DescriptionAttribute>()?.Description ?? prop.Name,
            };
            item.Set = s => prop.SetValue(s, item.Color);
            return item;
        });

    [Reactive] public required partial string Name { get; set; }
    [Reactive] public required partial string Color { get; set; }

    public Action<Settings> Set { get; private set; } = null!;
}
