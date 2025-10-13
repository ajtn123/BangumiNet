using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Html.Models;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Me;
using BangumiNet.Models;
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
    public UserViewModel(Api.P1.Models.Profile user)
    {
        Source = user;
        Avatar = user.Avatar;
        Sign = user.Sign;
        Nickname = user.Nickname;
        Username = user.Username;
        Id = user.Id;
        UserGroup = (UserGroup?)user.Group;
        if (user.JoinedAt != null)
            RegistrationTime = DateTimeOffset.FromUnixTimeSeconds((long)user.JoinedAt);

        Init();
    }
    public UserViewModel(Comment comment)
    {
        Source = comment;
        Username = comment.Username;
        Nickname = comment.Nickname;
        Avatar = new ImageSet() { Small = comment.AvatarUrl };

        Init();
    }
    public void Init()
    {
        Url ??= UrlProvider.BangumiTvUserUrlBase + Username;

        this.WhenAnyValue(x => x.Source).Subscribe(y => this.RaisePropertyChanged(nameof(IsMe)));

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new UserView() { DataContext = this } }.Show());
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Username));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvUserUrlBase + Username));

        if (Username != null)
        {
            WishList = new(ItemType.Subject, collectionType: CollectionType.Wish, username: Username);
            DoingList = new(ItemType.Subject, collectionType: CollectionType.Doing, username: Username);
            DoneList = new(ItemType.Subject, collectionType: CollectionType.Done, username: Username);
            DropList = new(ItemType.Subject, collectionType: CollectionType.Dropped, username: Username);
            HoldList = new(ItemType.Subject, collectionType: CollectionType.OnHold, username: Username);
            CharacterList = new(ItemType.Character, username: Username);
            PersonList = new(ItemType.Person, username: Username);
        }
    }
    public UserViewModel(string? username)
    {
        Source = username;
        Username = username;
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
    [Reactive] public partial SubjectCollectionListViewModel? WishList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? DoingList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? DoneList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? DropList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? HoldList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? CharacterList { get; set; }
    [Reactive] public partial SubjectCollectionListViewModel? PersonList { get; set; }

    public bool IsMe => Username == ApiC.CurrentUsername;
    public bool IsFull => Source is User or MeGetResponse;

    public Task<Bitmap?> AvatarSmall => ApiC.GetImageAsync(Avatar?.Small);
    public Task<Bitmap?> AvatarMedium => ApiC.GetImageAsync(Avatar?.Medium);
    public Task<Bitmap?> AvatarLarge => ApiC.GetImageAsync(Avatar?.Large);

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchWebCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }
}
