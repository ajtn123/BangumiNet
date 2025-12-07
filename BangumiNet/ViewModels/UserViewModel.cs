using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;

namespace BangumiNet.ViewModels;

public partial class UserViewModel : ItemViewModelBase
{
    public UserViewModel(User user)
    {
        Source = user;
        Avatar = user.Avatar;
        Sign = user.Sign;
        Username = user.Username;
        Nickname = user.Nickname;
        Id = user.Id;
        UserGroup = (UserGroup?)user.UserGroup;

        if (user.AdditionalData.TryGetValue("url", out var urlObj) && urlObj is string url)
            Url = url;

        Init();
    }
    public UserViewModel(MeGetResponse user)
    {
        Source = user;
        Avatar = user.Avatar;
        Sign = user.Sign;
        Nickname = user.Nickname;
        Username = user.Username;
        Id = user.Id;
        UserGroup = (UserGroup?)user.UserGroup;
        RegistrationTime = user.RegTime;
        Email = user.Email;
        TimeOffset = user.TimeOffset;

        if (user.AdditionalData.TryGetValue("url", out var urlObj) && urlObj is string url)
            Url = url;

        Init();
    }
    public UserViewModel(Api.P1.Models.Profile user)
    {
        Source = user;
        Avatar = user.Avatar;
        Sign = user.Sign;
        Nickname = user.Nickname;
        Username = user.Username;
        Id = user.Id;
        UserGroup = (UserGroup?)user.Group;
        RegistrationTime = CommonUtils.ParseBangumiTime(user.JoinedAt);

        Init();
    }
    public UserViewModel(Api.P1.Models.SlimUser user)
    {
        Source = user;
        Username = user.Username;
        Nickname = user.Nickname;
        Avatar = user.Avatar;
        Id = user.Id;
        UserGroup = (UserGroup?)user.Group;
        Sign = user.Sign;
        RegistrationTime = CommonUtils.ParseBangumiTime(user.JoinedAt);

        Init();
    }
    public UserViewModel(Api.P1.Models.User user)
    {
        Source = user;
        Username = user.Username;
        Nickname = user.Nickname;
        Avatar = user.Avatar;
        Id = user.Id;
        UserGroup = (UserGroup?)user.Group;
        Sign = user.Sign;
        Summary = user.Bio;
        RegistrationTime = CommonUtils.ParseBangumiTime(user.JoinedAt);

        Init();
    }
    public UserViewModel(Api.P1.Models.SimpleUser user)
    {
        Source = user;
        Username = user.Username;
        Nickname = user.Nickname;
        Id = user.Id;

        Init();
    }
    public void Init()
    {
        if (Username != null)
        {
            CollectionList = new(ItemType.Subject, collectionType: CollectionType.Doing, username: Username);
            Timeline = new() { Username = Username };
        }

        Title = $"{Nickname ?? Username ?? "用户"} - {Title}";
        Url ??= UrlProvider.BangumiTvUserUrlBase + Username;

        this.WhenAnyValue(x => x.Source).Subscribe(y => this.RaisePropertyChanged(nameof(IsMe)));

        SearchWebCommand = ReactiveCommand.Create(() => CommonUtils.SearchWeb(Username));
        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvUserUrlBase + Username));
    }
    public UserViewModel(string? username)
    {
        Source = username;
        Username = username;

        Init();
    }

    [Reactive] public partial IImages? Avatar { get; set; }
    [Reactive] public partial string? Sign { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? Url { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial string? Nickname { get; set; }
    [Reactive] public partial UserGroup? UserGroup { get; set; }
    [Reactive] public partial DateTimeOffset? RegistrationTime { get; set; }
    [Reactive] public partial string? Email { get; set; }
    [Reactive] public partial int? TimeOffset { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? CollectionList { get; set; }
    [Reactive] public partial TimelineViewModel? Timeline { get; set; }

    public bool IsMe => Username == ApiC.CurrentUsername;
    public bool IsFull => Source is Api.P1.Models.User;
}
