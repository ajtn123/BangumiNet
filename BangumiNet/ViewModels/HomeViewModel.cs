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
        CollectionListViewModel = new();

        this.WhenAnyValue(x => x.Me).Subscribe(x =>
        {
            _ = LoadMyCollection();
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

    private async Task LoadMyCollection()
    {
        if (Me?.Username is not { } username) return;

        var MyCollection = await ApiC.V0.Users[username].Collections.GetAsync(config =>
        {
            config.QueryParameters.SubjectType = (int)SubjectType.Anime;
            config.QueryParameters.Type = ((int)CollectionType.Doing).ToString();
            config.QueryParameters.Offset = 0;
            config.QueryParameters.Limit = 30;
        });

        if (MyCollection?.Data is not null)
            CollectionListViewModel?.AddSubjects(MyCollection);
    }
}
