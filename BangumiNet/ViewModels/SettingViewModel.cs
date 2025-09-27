using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel(Settings settings)
    {
        DefaultSettings = new();
        Settings = settings;
        Overrides = settings.GetOverrides();

        UserAgent = GetOverride(nameof(Settings.UserAgent));
        AuthToken = settings.AuthToken;
        BangumiTvUrlBase = GetOverride(nameof(Settings.BangumiTvUrlBase));
        DefaultSearchEngine = settings.DefaultSearchEngine;
        LocalDataDirectory = GetOverride(nameof(Settings.LocalDataDirectory));
        IsDiskCacheEnabled = settings.IsDiskCacheEnabled;
        DiskCacheSizeLimit = (long?)Overrides.TryGet(nameof(Settings.DiskCacheSizeLimit));
        PreferChineseNames = settings.PreferChineseNames;

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
        GetTokenCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Constants.BangumiTokenManagerUrl));
        SaveCommand = ReactiveCommand.Create(() =>
        {
            var newSettings = ToSettings();
            SettingProvider.UpdateSettings(newSettings);
            if (newSettings.AuthToken != Settings.AuthToken || newSettings.UserAgent != Settings.UserAgent)
                ApiC.RebuildClients();
        });

        SearchEngineSuggestions = [];
        this.WhenAnyValue(x => x.Settings).Subscribe(x => SearchEngineSuggestions = [.. x.SearchQueryUrlBases.Keys]);
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
        PreferChineseNames = PreferChineseNames,
        EpMainBg = GetColor(nameof(Settings.EpMainBg)),
        EpSpBg = GetColor(nameof(Settings.EpSpBg)),
        EpOpBg = GetColor(nameof(Settings.EpOpBg)),
        EpEdBg = GetColor(nameof(Settings.EpEdBg)),
        EpCmBg = GetColor(nameof(Settings.EpCmBg)),
        EpMadBg = GetColor(nameof(Settings.EpMadBg)),
        EpOtherBg = GetColor(nameof(Settings.EpOtherBg)),
        ErrorBg = GetColor(nameof(Settings.ErrorBg)),
        OkBg = GetColor(nameof(Settings.OkBg)),
        SearchQueryUrlBases = Settings.SearchQueryUrlBases,
    };

    public string GetColor(string key)
        => PaletteItems.First(x => x.Key == key).Color;
    public static string GetValue(string? value, string defaultValue)
        => string.IsNullOrWhiteSpace(value) ? defaultValue : value;
    public string GetOverride(string key)
        => Overrides.TryGet(key)?.ToString() ?? "";

    [Reactive] public partial Settings Settings { get; set; }
    [Reactive] public partial Dictionary<string, object?> Overrides { get; set; }
    [Reactive] public partial Settings DefaultSettings { get; set; }
    [Reactive] public partial string UserAgent { get; set; }
    [Reactive] public partial string? AuthToken { get; set; }
    [Reactive] public partial string BangumiTvUrlBase { get; set; }
    [Reactive] public partial string DefaultSearchEngine { get; set; }
    [Reactive] public partial string LocalDataDirectory { get; set; }
    [Reactive] public partial bool IsDiskCacheEnabled { get; set; }
    [Reactive] public partial long? DiskCacheSizeLimit { get; set; }
    [Reactive] public partial bool PreferChineseNames { get; set; }

    [Reactive] public partial List<string> SearchEngineSuggestions { get; set; }
    [Reactive] public partial List<PaletteItemViewModel> PaletteItems { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> UndoChangesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> RestoreCommand { get; set; }
    public ReactiveCommand<Unit, Unit> GetTokenCommand { get; set; }
}
public class PaletteItemViewModel : ViewModelBase
{
    public required string Name { get; set; }
    public required string Color { get; set; }
    public required string Key { get; set; }
}