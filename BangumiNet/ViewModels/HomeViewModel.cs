using BangumiNet.Api.ExtraEnums;

namespace BangumiNet.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    public HomeViewModel() => _ = Init();
    private async Task Init()
    {
        LoadGreeting();
        Today = await ApiC.GetViewModelAsync<CalendarViewModel>();
        Me = await ApiC.GetViewModelAsync<UserViewModel>();
        CollectionListViewModel = new(SubjectType.Anime, CollectionType.Doing);
        _ = CollectionListViewModel.LoadPageAsync(1);

        this.WhenAnyValue(x => x.Me).Subscribe(x =>
        {
            LoadGreeting();
        });
    }

    [Reactive] public partial UserViewModel? Me { get; set; }
    [Reactive] public partial string? Greeting { get; set; }
    [Reactive] public partial CalendarViewModel? Today { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? CollectionListViewModel { get; set; }

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
