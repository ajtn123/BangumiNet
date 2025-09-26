using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using ReactiveUI.SourceGenerators;

namespace BangumiNet.ViewModels;

public partial class UserViewModel : ViewModelBase
{
    public UserViewModel(User user)
    {
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

    public void Init()
    {
        Url ??= UrlProvider.BangumiTvUserUrlBase + Username;
    }

    [Reactive] public partial IImages? Avatar { get; set; }
    [Reactive] public partial string? Sign { get; set; }
    [Reactive] public partial string? Url { get; set; }
    [Reactive] public partial string? Username { get; set; }
    [Reactive] public partial string? Nickname { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial UserGroup? UserGroup { get; set; }
    [Reactive] public partial DateTimeOffset? RegistrationTime { get; set; }
    [Reactive] public partial string? Email { get; set; }
    [Reactive] public partial int? TimeOffset { get; set; }
}
