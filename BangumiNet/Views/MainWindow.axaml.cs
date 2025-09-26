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
            "主页" => new HomeView(),
            "每日放送" => new AiringView(),
            "搜索" => new SearchView(),
            "索引" => new SubjectBrowserView(),
            "设置" => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }

    private readonly NavigatorViewModel navigatorViewModel = new();
}
