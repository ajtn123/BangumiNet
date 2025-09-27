using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel(Settings settings)
    {
        DefaultSettings = new();
        Settings = settings;
        Overrides = settings.GetOverrides();

        UserAgent = Overrides.TryGet(nameof(Settings.UserAgent))?.ToString() ?? "";
        AuthToken = Overrides.TryGet(nameof(Settings.AuthToken))?.ToString() ?? "";
        BangumiTvUrlBase = Overrides.TryGet(nameof(Settings.BangumiTvUrlBase))?.ToString() ?? "";
        DefaultSearchEngine = settings.DefaultSearchEngine;
        LocalDataDirectory = Overrides.TryGet(nameof(Settings.LocalDataDirectory))?.ToString() ?? "";
        IsDiskCacheEnabled = settings.IsDiskCacheEnabled;
        DiskCacheSizeLimit = (long?)Overrides.TryGet(nameof(Settings.DiskCacheSizeLimit));
        PreferChineseNames = settings.PreferChineseNames;

        EpMainBg = Overrides.TryGet(nameof(Settings.EpMainBg))?.ToString() ?? "";
        EpOpBg = Overrides.TryGet(nameof(Settings.EpOpBg))?.ToString() ?? "";
        EpEdBg = Overrides.TryGet(nameof(Settings.EpEdBg))?.ToString() ?? "";
        EpCmBg = Overrides.TryGet(nameof(Settings.EpCmBg))?.ToString() ?? "";
        EpMadBg = Overrides.TryGet(nameof(Settings.EpMadBg))?.ToString() ?? "";
        EpOtherBg = Overrides.TryGet(nameof(Settings.EpOtherBg))?.ToString() ?? "";
        ErrorBg = Overrides.TryGet(nameof(Settings.ErrorBg))?.ToString() ?? "";
        OkBg = Overrides.TryGet(nameof(Settings.OkBg))?.ToString() ?? "";

        SaveCommand = ReactiveCommand.Create(() => SettingProvider.UpdateSettings(ToSettings()));
        UndoChangesCommand = ReactiveCommand.Create(() => { });
        RestoreCommand = ReactiveCommand.Create(() => { });
        GetTokenCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Constants.BangumiTokenManagerUrl));

        SearchEngineSuggestions = [];
        this.WhenAnyValue(x => x.Settings).Subscribe(x => SearchEngineSuggestions = [.. x.SearchQueryUrlBases.Keys]);
    }

    public Settings ToSettings() => new()
    {
        UserAgent = UserAgent,
        AuthToken = AuthToken,
        BangumiTvUrlBase = BangumiTvUrlBase,
        DefaultSearchEngine = DefaultSearchEngine,
        LocalDataDirectory = LocalDataDirectory,
        IsDiskCacheEnabled = IsDiskCacheEnabled,
        DiskCacheSizeLimit = DiskCacheSizeLimit ?? 0,
        PreferChineseNames = PreferChineseNames,
        EpMainBg = EpMainBg,
        EpOpBg = EpOpBg,
        EpEdBg = EpEdBg,
        EpCmBg = EpCmBg,
        EpMadBg = EpMadBg,
        EpOtherBg = EpOtherBg,
        ErrorBg = ErrorBg,
        OkBg = OkBg,
    };

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
    [Reactive] public partial string EpMainBg { get; set; }
    [Reactive] public partial string EpOpBg { get; set; }
    [Reactive] public partial string EpEdBg { get; set; }
    [Reactive] public partial string EpCmBg { get; set; }
    [Reactive] public partial string EpMadBg { get; set; }
    [Reactive] public partial string EpOtherBg { get; set; }
    [Reactive] public partial string ErrorBg { get; set; }
    [Reactive] public partial string OkBg { get; set; }

    [Reactive] public partial List<string> SearchEngineSuggestions { get; set; }
    [Reactive] public partial List<PaletteItem> PaletteItems { get; set; }

    public ReactiveCommand<Unit, Unit> SaveCommand { get; set; }
    public ReactiveCommand<Unit, Unit> UndoChangesCommand { get; set; }
    public ReactiveCommand<Unit, Unit> RestoreCommand { get; set; }
    public ReactiveCommand<Unit, Unit> GetTokenCommand { get; set; }

    public class PaletteItem
    {
        public required string Name { get; set; }
        public required string Color { get; set; }
        public required string Key { get; set; }
    }
}
