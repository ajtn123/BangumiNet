using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reflection;

namespace BangumiNet.Views;

public partial class SettingView : ReactiveUserControl<SettingViewModel>
{
    private readonly CompositeDisposable disposables = [];
    public SettingView()
    {
        if (Design.IsDesignMode) DataContext = new SettingViewModel(SettingProvider.CurrentSettings);
        InitializeComponent();
        VersionText.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        Credits.ItemsSource = AppDomain.CurrentDomain.GetAssemblies().Select(x =>
        {
            var name = x.GetName();
            return new CreditItem() { Name = $"{name.Name} - {name.Version}", Tooltip = name.FullName, Type = "依赖" };
        }).Where(x => x.Name != "Anonymously Hosted DynamicMethods Assembly - 0.0.0.0").OrderBy(x => x.Name).Union([
            new CreditItem() { Name = $"[PIXIV 22876424] 何番煎じだかわからないけど", Tooltip = "https://www.pixiv.net/artworks/22876424", Type = "资产" },
            new CreditItem() { Name = $"Fluent UI System Icons", Tooltip = "https://github.com/microsoft/fluentui-system-icons", Type = "资产" },
            new CreditItem() { Name = $"MingCute Icon tv-2-line", Tooltip = "https://www.mingcute.com", Type = "资产" },
            new CreditItem() { Name = $"Bangumi Stickers", Tooltip = "https://bgm.tv", Type = "资产" },
            new CreditItem() { Name = $"Bangumi Open API", Tooltip = "https://bangumi.github.io/api/#/", Type = "服务" },
            new CreditItem() { Name = $"Bangumi Private API", Tooltip = "https://next.bgm.tv/p1/#/", Type = "服务" },
            new CreditItem() { Name = $"Bangumi Data", Tooltip = "https://github.com/bangumi-data/bangumi-data", Type = "服务" },
        ]);
        this.WhenAnyValue(x => x.ViewModel)
            .WhereNotNull()
            .Subscribe(vm =>
            {
                disposables.Clear();
                vm.RestoreCommand.Subscribe(a => DataContext = new SettingViewModel(new() { AuthToken = SettingProvider.CurrentSettings.AuthToken })).DisposeWith(disposables);
                vm.UndoChangesCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.CurrentSettings)).DisposeWith(disposables);
                vm.SaveCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.CurrentSettings)).DisposeWith(disposables);
            });
    }

    private void LocalDataPickDir(object? sender, RoutedEventArgs e)
        => _ = LocalDataPickDirAsync();
    private void OpenGitHub(object? sender, RoutedEventArgs e)
        => CommonUtils.OpenUrlInBrowser(Shared.Constants.SourceRepository);

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
    public required string Type { get; set; }
    public required string Name { get; set; }
    public required string Tooltip { get; set; }
}
