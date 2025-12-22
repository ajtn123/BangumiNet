using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reflection;

namespace BangumiNet.Views;

public partial class SettingView : ReactiveUserControl<SettingViewModel>
{
    private readonly CompositeDisposable commandSubs = [];
    public SettingView()
    {
        if (Design.IsDesignMode) DataContext = new SettingViewModel(SettingProvider.Current);

        InitializeComponent();

        VersionText.Text = Assembly.GetEntryAssembly()!.GetName().Version?.ToString();
        ApplicationThemeComboBox.ItemsSource = Enum.GetValues<ApplicationTheme>();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(x => x.ViewModel)
                .WhereNotNull()
                .Subscribe(vm =>
                {
                    commandSubs.Clear();
                    vm.RestoreCommand.Subscribe(a => DataContext = new SettingViewModel(new() { AuthToken = SettingProvider.Current.AuthToken })).DisposeWith(commandSubs);
                    vm.UndoChangesCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.Current)).DisposeWith(commandSubs);
                    vm.SaveCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.Current)).DisposeWith(commandSubs);
                }).DisposeWith(disposables);

            _ = CheckUpdate(ShowUpdate);
        });
    }

#if !DEBUG
    private static bool isUpdateChecked;
    private static Version? latestVersion;
#endif
    public static async Task CheckUpdate(Action<Version> callback)
    {
#if !DEBUG
        if (!isUpdateChecked)
        {
            isUpdateChecked = true;
            latestVersion = await Updater.CheckWith(ApiC.HttpClient);
        }

        if (latestVersion != null) callback(latestVersion);
#endif
    }

    private void ShowUpdate(Version version)
    {
        LatestVersionText.Text = version.ToString();
        UpdateInfo.IsVisible = true;
    }

    private void LocalDataPickDir(object? sender, RoutedEventArgs e)
        => _ = LocalDataPickDirAsync();
    private void OpenGitHub(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUrlInBrowser(Constants.SourceRepository);
    private void OpenGitHubLatestRelease(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUrlInBrowser(Constants.SourceRepositoryLatestRelease);

    private async Task LocalDataPickDirAsync()
    {
        var sp = TopLevel.GetTopLevel(this)?.StorageProvider;
        if (sp == null) return;
        var defaultLocation = ViewModel?.DefaultSettings.LocalDataDirectory ?? "";
        var pickedL = await sp.OpenFolderPickerAsync(new()
        {
            AllowMultiple = false,
            Title = "选择缓存位置",
            SuggestedStartLocation = await sp.TryGetFolderFromPathAsync(defaultLocation)
        });
        if (pickedL != null && pickedL.Count > 0)
            ViewModel?.LocalDataDirectory = pickedL[0].TryGetLocalPath() ?? "";
    }
}
public record class CreditItem()
{
    public static readonly CreditItem[] Items;
    static CreditItem()
    {
        Items = [
            new CreditItem() { Name = $"Bangumi Open API", Tooltip = "https://bangumi.github.io/api/#/", Type = "服务" },
            new CreditItem() { Name = $"Bangumi Private API", Tooltip = "https://next.bgm.tv/p1/#/", Type = "服务" },
            new CreditItem() { Name = $"Bangumi Stickers", Tooltip = "https://bgm.tv", Type = "资产" },
            new CreditItem() { Name = $"Bangumi Data", Tooltip = "https://github.com/bangumi-data/bangumi-data", Type = "服务" },
            new CreditItem() { Name = $"Fluent UI System Icons", Tooltip = "https://github.com/microsoft/fluentui-system-icons", Type = "资产" },
            new CreditItem() { Name = $"はらぺこ 何番煎じだかわからないけど", Tooltip = "https://www.pixiv.net/artworks/22876424", Type = "资产" },
            new CreditItem() { Name = $"MingCute Icon tv-2-line", Tooltip = "https://www.mingcute.com", Type = "资产" },
            .. AppDomain.CurrentDomain.GetAssemblies().Select(x =>
            {
                var name = x.GetName();
                return new CreditItem() { Name = $"{name.Name} - {name.Version}", Tooltip = name.FullName, Type = "依赖" };
            }).Where(x => !x.Name.StartsWith("Anonymously")).OrderBy(x => x.Name)
        ];
    }

    public required string Type { get; set; }
    public required string Name { get; set; }
    public required string Tooltip { get; set; }
}
