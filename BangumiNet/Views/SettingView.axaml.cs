using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System.Reflection;

namespace BangumiNet.Views;

public partial class SettingView : ReactiveUserControl<SettingViewModel>
{
    public SettingView()
    {
        if (Design.IsDesignMode) DataContext = new SettingViewModel(SettingProvider.CurrentSettings);
        InitializeComponent();
        VersionText.Text = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
        Deps.ItemsSource = AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName())
            .Where(x => x.Name != "Anonymously Hosted DynamicMethods Assembly").OrderBy(x => x.Name);
        this.WhenAnyValue(x => x.ViewModel).Subscribe(y =>
        {
            y?.RestoreCommand.Subscribe(a => DataContext = new SettingViewModel(new() { AuthToken = SettingProvider.CurrentSettings.AuthToken }));
            y?.UndoChangesCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.CurrentSettings));
            y?.SaveCommand.Subscribe(a => DataContext = new SettingViewModel(SettingProvider.CurrentSettings));
        });
    }

    private void LocalDataPickDir(object? sender, RoutedEventArgs e)
        => _ = LocalDataPickDirAsync();
    private void OpenGitHub(object? sender, RoutedEventArgs e)
        => Common.OpenUrlInBrowser(Constants.SourceRepository);

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