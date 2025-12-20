using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace BangumiNet.Utils;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data) => data switch
    {
        TextViewModel => new TextView { DataContext = data },
        HomeViewModel => new HomeView { DataContext = data },
        AiringViewModel => new AiringView { DataContext = data },
        MeViewModel => new MeView { DataContext = data },
        SearchViewModel => new SearchView { DataContext = data },
        SubjectBrowserViewModel => new SubjectBrowserView { DataContext = data },
        BangumiDataIndexViewModel => new BangumiDataIndexView { DataContext = data },
        SubjectCollectionListViewModel => new SubjectCollectionListView { DataContext = data },
        SettingViewModel => new SettingView { DataContext = data },
        TimelineViewModel => new TimelineView { DataContext = data },
        CalendarViewModel => new CalendarView { DataContext = data },
        TrendingViewModel => new TrendingView { DataContext = data },
        GroupHomeViewModel => new GroupHomeView { DataContext = data },
        CommentListViewModel => new CommentListView { DataContext = data },
        RevisionListViewModel => new RevisionListView { DataContext = data },
        NotificationListViewModel => new NotificationListView { DataContext = data },
        ItemNetworkViewModel => new ItemNetworkView { DataContext = data },
        _ => new TextBlock { Text = $"DataTemplate Not Found: {data?.GetType().FullName}" }
    };

    public bool Match(object? data)
        => data is ViewModelBase;
}

public class ItemViewLocator : IDataTemplate
{
    public Control? Build(object? data) => data switch
    {
        SubjectViewModel => new SubjectView { DataContext = data },
        CharacterViewModel => new CharacterView { DataContext = data },
        PersonViewModel => new PersonView { DataContext = data },
        EpisodeViewModel => new EpisodeFullView { DataContext = data },
        UserViewModel => new UserView { DataContext = data },
        GroupViewModel => new GroupView { DataContext = data },
        TopicViewModel => new TopicView { DataContext = data },
        BlogViewModel => new BlogView { DataContext = data },
        IndexViewModel => new IndexView { DataContext = data },
        RevisionViewModel => new RevisionView { DataContext = data },
        _ => new TextBlock { Text = $"DataTemplate Not Found: {data?.GetType().FullName}" }
    };

    public bool Match(object? data)
        => data is ItemViewModelBase;
}
