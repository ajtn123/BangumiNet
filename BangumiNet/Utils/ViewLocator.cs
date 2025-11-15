using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace BangumiNet.Utils;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data) => data switch
    {
        HomeViewModel => new HomeView { DataContext = data },
        AiringViewModel => new AiringView { DataContext = data },
        MeViewModel => new MeView { DataContext = data },
        SearchViewModel => new SearchView { DataContext = data },
        SubjectBrowserViewModel => new SubjectBrowserView { DataContext = data },
        SubjectCollectionListViewModel => new SubjectCollectionListView { DataContext = data },
        SettingViewModel => new SettingView { DataContext = data },
        TimelineViewModel => new TimelineView { DataContext = data },
        CalendarViewModel => new CalendarView { DataContext = data },
        TrendingViewModel => new TrendingView { DataContext = data },
        GroupHomeViewModel => new GroupHomeView { DataContext = data },
        CommentListViewModel => new CommentListView { DataContext = data },
        _ => new TextBlock { Text = $"DataTemplate Not Found: {data?.GetType().FullName}" }
    };

    public bool Match(object? data)
        => data is ViewModelBase;
}
