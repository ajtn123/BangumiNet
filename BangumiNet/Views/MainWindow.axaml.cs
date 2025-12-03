using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnAppStartup)
            SplashScreen = new WindowSplashScreen(this);

        homeVM = new();
        NavView.Content = homeVM;

        Navigator.AsyncPopulator = navigatorViewModel.PopulateAsync;

        NavView.ItemInvoked += (s, e) =>
        {
            if (e.InvokedItem is string tab)
                SwitchView(tab);
        };
    }
    private string currentView = "主页";
    public async void SwitchView(string view, CancellationToken cancellationToken = default)
    {
        if (view == currentView) return;
        currentView = view;
        NavView.Content = view switch
        {
            "主页" => homeVM ??= new(),
            "每日放送" => airingVM ??= await ApiC.GetViewModelAsync<AiringViewModel>(cancellationToken: cancellationToken),
            "小组" => groupVM ??= new(),
            "搜索" => searchVM ??= new(),
            "索引" => subjectBrowserVM ??= new(),
            "番组索引" => bangumiDataIndexVM ??= new(),
            "我" => meVM ??= await ApiC.GetViewModelAsync<MeViewModel>(cancellationToken: cancellationToken),
            "设置" => new SettingViewModel(SettingProvider.CurrentSettings),
            _ => throw new NotImplementedException(),
        };
    }

    public readonly NavigatorViewModel navigatorViewModel = new();
    public HomeViewModel? homeVM;
    public GroupHomeViewModel? groupVM;
    public SearchViewModel? searchVM;
    public SubjectBrowserViewModel? subjectBrowserVM;
    public BangumiDataIndexViewModel? bangumiDataIndexVM;
    public AiringViewModel? airingVM;
    public MeViewModel? meVM;
}
