using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        if (SettingProvider.Current.ShowSplashScreenOnAppStartup)
            SplashScreen = new WindowSplashScreen(this);

        Navigator.AsyncPopulator = navigatorViewModel.PopulateAsync;

        SwitchTab(SettingProvider.Current.StartupTab);

        if (NavView.MenuItems.OfType<NavigationViewItemBase>().FirstOrDefault(x => x.Content as string == currentTab.ToString()) is { } item)
            item.IsSelected = true;
        else if (NavView.FooterMenuItems.OfType<NavigationViewItemBase>().FirstOrDefault(x => x.Content as string == currentTab.ToString()) is { } itemF)
            itemF.IsSelected = true;

        NavView.ItemInvoked += (s, e) =>
        {
            if (e.InvokedItem is string tabName && Enum.TryParse<MainWindowTab>(tabName, out var tab))
                SwitchTab(tab);
        };
    }

    private MainWindowTab currentTab = (MainWindowTab)(-1);
    public async void SwitchTab(MainWindowTab target, CancellationToken cancellationToken = default)
    {
        if (target == currentTab) return;
        currentTab = target;
        NavView.Content = target switch
        {
            MainWindowTab.主页 => homeVM ??= new(),
            MainWindowTab.每日放送 => airingVM ??= new(),
            MainWindowTab.小组 => groupVM ??= new(),
            MainWindowTab.搜索 => searchVM ??= new(),
            MainWindowTab.分类浏览 => subjectBrowserVM ??= new(),
            MainWindowTab.番组索引 => bangumiDataIndexVM ??= new(),
            MainWindowTab.库 => libraryVM ??= new(),

            MainWindowTab.我 => meVM ??= await ApiC.GetViewModelAsync<MeViewModel>(cancellationToken: cancellationToken),
            MainWindowTab.设置 => new SettingViewModel(SettingProvider.Current),
            _ => throw new NotImplementedException(),
        };
    }

    public readonly NavigatorViewModel navigatorViewModel = new();
    public HomeViewModel? homeVM;
    public GroupHomeViewModel? groupVM;
    public SearchViewModel? searchVM;
    public SubjectBrowserViewModel? subjectBrowserVM;
    public BangumiDataIndexViewModel? bangumiDataIndexVM;
    public LibraryHomeViewModel? libraryVM;
    public AiringViewModel? airingVM;
    public MeViewModel? meVM;

    public static MainWindow Instance => (MainWindow)((IClassicDesktopStyleApplicationLifetime)Application.Current?.ApplicationLifetime!).MainWindow!;

    public static void ShowInfo(InfoBarSeverity severity = InfoBarSeverity.Informational, string? title = "信息", string? message = null, Control? action = null)
    {
        var info = Instance.Info;
        info.Severity = severity;
        info.Title = title;
        info.Message = message;
        info.ActionButton = action;
        info.IsOpen = true;
    }
}
