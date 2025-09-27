using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

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

    private readonly NavigatorViewModel navigatorViewModel = new();
    private HomeView? homeView;
    private SearchView? searchView;
    private SubjectBrowserView? subjectBrowserView;
    private AiringView? airingView;
    private UserView? meView;
}
