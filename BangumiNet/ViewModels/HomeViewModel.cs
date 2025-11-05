using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    public HomeViewModel()
    {
        LoadGreeting();
        this.WhenAnyValue(x => x.Me).Subscribe(x => LoadGreeting());
        TimelineViewModel = new();
        TrendingViewModel = new(ItemType.Subject, SubjectType.Anime);
        CollectionListViewModel = new(ItemType.Subject, SubjectType.Anime, CollectionType.Doing);
        TimelineViewModel.LoadCommand.Execute(null);
        TrendingViewModel.LoadCommand.Execute(1);
        _ = Task.Run(async () => Today = await ApiC.GetViewModelAsync<CalendarViewModel>());
        _ = Task.Run(async () =>
        {
            Me = await ApiC.GetViewModelAsync<UserViewModel>();
            await CollectionListViewModel.LoadPageAsync(1);
        });
    }

    [Reactive] public partial UserViewModel? Me { get; set; }
    [Reactive] public partial string? Greeting { get; set; }
    [Reactive] public partial CalendarViewModel? Today { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? CollectionListViewModel { get; set; }
    [Reactive] public partial TimelineViewModel TimelineViewModel { get; set; }
    [Reactive] public partial TrendingViewModel TrendingViewModel { get; set; }

    private void LoadGreeting()
    {
        var greeting = "";

        if (Me is null)
            greeting += $"你好！";
        else
            greeting += $"你好，{Me.Nickname}！";

        greeting += $"今天是{DateTime.Now:D}。";

        Greeting = greeting;
    }
}
