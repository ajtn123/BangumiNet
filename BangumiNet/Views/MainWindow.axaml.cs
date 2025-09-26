using Avalonia.Controls;

namespace BangumiNet.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        //NavigatorButton.Click += (s, e) => new NavigatorWindow() { DataContext = new NavigatorViewModel() }.Show();

        NavView.ItemInvoked += (s, e) =>
        {
            if (e.InvokedItem is string tab)
            {
                if (tab == currentItem) return;
                currentItem = tab;
                NavView.Content = tab switch
                {
                    "主页" => new HomeView(),
                    "每日放送" => new AiringView(),
                    "搜索" => new SearchView(),
                    "索引" => new SubjectBrowserView(),
                    _ => throw new NotImplementedException(),
                };
            }
        };
    }

    private string currentItem = "主页";
}