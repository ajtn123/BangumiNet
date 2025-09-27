using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class SettingViewModel : ViewModelBase
{
    public SettingViewModel(Settings settings)
    {
        Settings = settings;
        UserAgent = settings.UserAgent;
        AuthToken = settings.AuthToken;
        BangumiTvUrlBase = settings.BangumiTvUrlBase;
        DefaultSearchEngine = settings.DefaultSearchEngine;
        LocalDataDirectory = settings.LocalDataDirectory;
        IsDiskCacheEnabled = settings.IsDiskCacheEnabled;
        DiskCacheSizeLimit = settings.DiskCacheSizeLimit;
        PreferChineseNames = settings.PreferChineseNames;
        EpMainBg = settings.EpMainBg;
        EpOpBg = settings.EpOpBg;
        EpEdBg = settings.EpEdBg;
        EpCmBg = settings.EpCmBg;
        EpMadBg = settings.EpMadBg;
        EpOtherBg = settings.EpOtherBg;
        ErrorBg = settings.ErrorBg;
        OkBg = settings.OkBg;

        SaveCommand = ReactiveCommand.Create(() => SettingProvider.UpdateSettings(ToSettings()));
        UndoChangesCommand = ReactiveCommand.Create(() => { });
        RestoreCommand = ReactiveCommand.Create(() => { });
    }

    public Settings ToSettings() => new()
    {
        UserAgent = UserAgent,
        AuthToken = AuthToken,
        BangumiTvUrlBase = BangumiTvUrlBase,
        DefaultSearchEngine = DefaultSearchEngine,
        LocalDataDirectory = LocalDataDirectory,
        IsDiskCacheEnabled = IsDiskCacheEnabled,
        DiskCacheSizeLimit = DiskCacheSizeLimit,
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
    [Reactive] public partial string UserAgent { get; set; }
    [Reactive] public partial string? AuthToken { get; set; }
    [Reactive] public partial string BangumiTvUrlBase { get; set; }
    [Reactive] public partial string DefaultSearchEngine { get; set; }
    [Reactive] public partial string LocalDataDirectory { get; set; }
    [Reactive] public partial bool IsDiskCacheEnabled { get; set; }
    [Reactive] public partial long DiskCacheSizeLimit { get; set; }
    [Reactive] public partial bool PreferChineseNames { get; set; }
    [Reactive] public partial string EpMainBg { get; set; }
    [Reactive] public partial string EpOpBg { get; set; }
    [Reactive] public partial string EpEdBg { get; set; }
    [Reactive] public partial string EpCmBg { get; set; }
    [Reactive] public partial string EpMadBg { get; set; }
    [Reactive] public partial string EpOtherBg { get; set; }
    [Reactive] public partial string ErrorBg { get; set; }
    [Reactive] public partial string OkBg { get; set; }

    public ICommand SaveCommand { get; set; }
    public ICommand UndoChangesCommand { get; set; }
    public ICommand RestoreCommand { get; set; }
}
