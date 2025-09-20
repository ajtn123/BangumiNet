using Avalonia.Media;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using ReactiveUI.SourceGenerators;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    public HomeViewModel() => _ = Init();
    private async Task Init()
    {
        LoadGreeting();
        Me = await ApiC.V0.Me.GetAsMeGetResponseAsync();
        SubjectCollectionViewModel = new();
    }

    public MeGetResponse? Me { get; set { field = value; LoadGreeting(); _ = LoadAvatar(); _ = LoadCalendar(); _ = LoadMyCollection(); } }

    [Reactive] public partial string? Greeting { get; set; }
    [Reactive] public partial IImage? Avatar { get; set; }
    [Reactive] public partial IEnumerable<CalendarViewModel>? Calendars { get; set; }
    [Reactive] public partial CalendarViewModel? Today { get; set; }
    [Reactive] public partial Paged_UserCollection? MyCollection { get; set; }
    [Reactive] public partial SubjectCollectionViewModel? SubjectCollectionViewModel { get; set; }

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

    private async Task LoadAvatar()
        => Avatar = await ApiC.GetImageAsync(Me?.Avatar?.Small);

    private async Task LoadMyCollection()
    {
        if (Me?.Username is not { } username) return;

        MyCollection = await ApiC.V0.Users[username].Collections.GetAsync(config =>
        {
            config.QueryParameters.SubjectType = (int)SubjectType.Anime;
            config.QueryParameters.Type = ((int)CollectionType.Wish).ToString();
            config.QueryParameters.Offset = 0;
            config.QueryParameters.Limit = 30;
        });

        if (MyCollection?.Data is not null)
            SubjectCollectionViewModel?.Collections = MyCollection.Data.Select(c => new SubjectViewModel(c.Subject!)).ToObservableCollection();
    }

    private async Task LoadCalendar()
    {
        Calendars = (await ApiC.Clients.LegacyClient.Calendar.GetAsync())?.Select(c => new CalendarViewModel(c));
        Today = Calendars?.Where(c => c.DayOfWeek == DateTime.Today.DayOfWeek).First();
    }
}
