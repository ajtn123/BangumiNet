using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class UserViewModel : ViewModelBase
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

    public void Init()
    {
        Url ??= UrlProvider.BangumiTvUserUrlBase + Username;

        this.WhenAnyValue(x => x.Source).Subscribe(y => this.RaisePropertyChanged(nameof(IsMe)));

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new UserView() { DataContext = this } }.Show());
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Username));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvUserUrlBase + Username));
    }

    [Reactive] public partial object? Source { get; set; }
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

    public bool IsMe => Source is MeGetResponse;

    public Task<Bitmap?> AvatarSmall => ApiC.GetImageAsync(Avatar?.Small);
    public Task<Bitmap?> AvatarMedium => ApiC.GetImageAsync(Avatar?.Medium);
    public Task<Bitmap?> AvatarLarge => ApiC.GetImageAsync(Avatar?.Large);

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchWebCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }
}
