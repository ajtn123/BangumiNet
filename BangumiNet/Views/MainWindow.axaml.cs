using FluentAvalonia.UI.Windowing;

namespace BangumiNet.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        InitializeComponent();

        if (SettingProvider.CurrentSettings.ShowSplashScreenOnAppStartup)
            SplashScreen = new WindowSplashScreen(this);

        homeView = new();
        NavView.Content = homeView;

        Navigator.AsyncPopulator = navigatorViewModel.PopulateAsync;

        NavView.ItemInvoked += (s, e) =>
        {
            if (e.InvokedItem is string tab)
                SwitchView(tab);
        };
    }
    private string currentView = "主页";
    public void SwitchView(string view)
    {
        if (view == currentView) return;
        currentView = view;
        NavView.Content = view switch
        {
            "主页" => homeView ??= new HomeView(),
            "每日放送" => airingView ??= new AiringView(),
            "搜索" => searchView ??= new SearchView(),
            "索引" => subjectBrowserView ??= new SubjectBrowserView(),
            "我" => meView ??= new UserView(loadMe: true),
            "设置" => new SettingView() { DataContext = new SettingViewModel(SettingProvider.CurrentSettings) },
            _ => throw new NotImplementedException(),
        };
    }

    public readonly NavigatorViewModel navigatorViewModel = new();
    public HomeView? homeView;
    public SearchView? searchView;
    public SubjectBrowserView? subjectBrowserView;
    public AiringView? airingView;
    public UserView? meView;
}
