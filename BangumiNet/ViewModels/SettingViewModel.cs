using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel(Settings settings)
    {
        DefaultSettings = new();
        Source = settings;
        Overrides = settings.GetOverrides();

        Title = $"设置 - {Title}";
        SearchEngineSuggestions = [];

        UserAgent = GetOverride(nameof(Source.UserAgent));
        AuthToken = settings.AuthToken;
        BangumiTvUrlBase = GetOverride(nameof(Source.BangumiTvUrlBase));
        DefaultSearchEngine = settings.DefaultSearchEngine;
        LocalDataDirectory = GetOverride(nameof(Source.LocalDataDirectory));
        IsDiskCacheEnabled = settings.IsDiskCacheEnabled;
        DiskCacheSizeLimit = (long?)Overrides.TryGet(nameof(Source.DiskCacheSizeLimit));
        CollectionPageSize = (int?)Overrides.TryGet(nameof(Source.CollectionPageSize));
        RevisionPageSize = (int?)Overrides.TryGet(nameof(Source.RevisionPageSize));
        CommentPageSize = (int?)Overrides.TryGet(nameof(settings.CommentPageSize));
        SearchPageSize = (int?)Overrides.TryGet(nameof(Source.SearchPageSize));
        EpisodePageSize = (int?)Overrides.TryGet(nameof(Source.EpisodePageSize));
        SubjectBrowserPageSize = (int?)Overrides.TryGet(nameof(Source.SubjectBrowserPageSize));
        PreferChineseNames = settings.PreferChineseNames;
        ShowSplashScreenOnAppStartup = settings.ShowSplashScreenOnAppStartup;
        ShowSplashScreenOnWindowStartup = settings.ShowSplashScreenOnWindowStartup;

        PaletteItems = [
            new() { Name = "", Color = settings.EpMainBg, Key = nameof(settings.EpMainBg) },
            new() { Name = "", Color = settings.EpSpBg, Key = nameof(settings.EpSpBg) },
            new() { Name = "", Color = settings.EpOpBg, Key = nameof(settings.EpOpBg) },
            new() { Name = "", Color = settings.EpEdBg, Key = nameof(settings.EpEdBg) },
            new() { Name = "", Color = settings.EpCmBg, Key = nameof(settings.EpCmBg) },
            new() { Name = "", Color = settings.EpMadBg, Key = nameof(settings.EpMadBg) },
            new() { Name = "", Color = settings.EpOtherBg, Key = nameof(settings.EpOtherBg) },
            new() { Name = "", Color = settings.ErrorBg, Key = nameof(settings.ErrorBg) },
            new() { Name = "", Color = settings.OkBg, Key = nameof(settings.OkBg) },
        ];

        UndoChangesCommand = ReactiveCommand.Create(() => { });
        RestoreCommand = ReactiveCommand.Create(() => { });
        GetTokenCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(Constants.BangumiTokenManagerUrl));
        SaveCommand = ReactiveCommand.Create(() =>
        {
            var newSettings = ToSettings();
            SettingProvider.UpdateSettings(newSettings);
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

        this.WhenAnyValue(x => x.Source).Subscribe(x => SearchEngineSuggestions = [.. x.SearchQueryUrlBases.Keys]);
    }

    public Settings ToSettings() => new()
    {
        UserAgent = GetValue(UserAgent, DefaultSettings.UserAgent),
        AuthToken = AuthToken,
        BangumiTvUrlBase = GetValue(BangumiTvUrlBase, DefaultSettings.BangumiTvUrlBase),
        DefaultSearchEngine = DefaultSearchEngine,
        LocalDataDirectory = GetValue(LocalDataDirectory, DefaultSettings.LocalDataDirectory),
        IsDiskCacheEnabled = IsDiskCacheEnabled,
        DiskCacheSizeLimit = DiskCacheSizeLimit ?? DefaultSettings.DiskCacheSizeLimit,
        CollectionPageSize = CollectionPageSize ?? DefaultSettings.CollectionPageSize,
        RevisionPageSize = RevisionPageSize ?? DefaultSettings.RevisionPageSize,
        CommentPageSize = CommentPageSize ?? DefaultSettings.CommentPageSize,
        SearchPageSize = SearchPageSize ?? DefaultSettings.SearchPageSize,
        EpisodePageSize = EpisodePageSize ?? DefaultSettings.EpisodePageSize,
        SubjectBrowserPageSize = SubjectBrowserPageSize ?? DefaultSettings.SubjectBrowserPageSize,
        PreferChineseNames = PreferChineseNames,
        ShowSplashScreenOnAppStartup = ShowSplashScreenOnAppStartup,
        ShowSplashScreenOnWindowStartup = ShowSplashScreenOnWindowStartup,
        EpMainBg = GetColor(nameof(Source.EpMainBg)),
        EpSpBg = GetColor(nameof(Source.EpSpBg)),
        EpOpBg = GetColor(nameof(Source.EpOpBg)),
        EpEdBg = GetColor(nameof(Source.EpEdBg)),
        EpCmBg = GetColor(nameof(Source.EpCmBg)),
        EpMadBg = GetColor(nameof(Source.EpMadBg)),
        EpOtherBg = GetColor(nameof(Source.EpOtherBg)),
        ErrorBg = GetColor(nameof(Source.ErrorBg)),
        OkBg = GetColor(nameof(Source.OkBg)),
        SearchQueryUrlBases = Source.SearchQueryUrlBases,
    };

    public string GetColor(string key)
        => PaletteItems.First(x => x.Key == key).Color;
    public static string GetValue(string? value, string defaultValue)
        => string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    public string GetOverride(string key)
        => Overrides.TryGet(key)?.ToString() ?? "";

    [Reactive] public partial Settings Source { get; set; }
    [Reactive] public partial Dictionary<string, object?> Overrides { get; set; }
    [Reactive] public partial Settings DefaultSettings { get; set; }
    [Reactive] public partial string UserAgent { get; set; }
    [Reactive] public partial string? AuthToken { get; set; }
    [Reactive] public partial string BangumiTvUrlBase { get; set; }
    [Reactive] public partial string DefaultSearchEngine { get; set; }
    [Reactive] public partial string LocalDataDirectory { get; set; }
    [Reactive] public partial bool IsDiskCacheEnabled { get; set; }
    [Reactive] public partial long? DiskCacheSizeLimit { get; set; }
    [Reactive] public partial int? CollectionPageSize { get; set; }
    [Reactive] public partial int? RevisionPageSize { get; set; }
    [Reactive] public partial int? CommentPageSize { get; set; }
    [Reactive] public partial int? SearchPageSize { get; set; }
    [Reactive] public partial int? EpisodePageSize { get; set; }
    [Reactive] public partial int? SubjectBrowserPageSize { get; set; }
    [Reactive] public partial bool PreferChineseNames { get; set; }
    [Reactive] public partial bool ShowSplashScreenOnAppStartup { get; set; }
    [Reactive] public partial bool ShowSplashScreenOnWindowStartup { get; set; }

    [Reactive] public partial List<string> SearchEngineSuggestions { get; set; }
    [Reactive] public partial List<PaletteItemViewModel> PaletteItems { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> UndoChangesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> RestoreCommand { get; set; }
    public ReactiveCommand<Unit, Unit> GetTokenCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DumpCacheCommand { get; set; }

    public static string CacheSizeString => $"{CacheProvider.CacheSize} /";
}
public class PaletteItemViewModel : ViewModelBase
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required string Key { get; set; }
}
