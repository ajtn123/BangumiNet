using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Followers;
using BangumiNet.Api.P1.P1.Friends;
using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class UserListViewModel : SubjectListPagedViewModel
{
    public UserListViewModel(UserRelationType type)
    {
        ListType = type;
        PageNavigator.IsVisible = ListType switch
        {
            UserRelationType.Friend => true,
            UserRelationType.Follower => true,
            UserRelationType.Blocked => false,
            _ => throw new NotImplementedException(),
        };
        Title = ListType switch
        {
            UserRelationType.Friend => $"好友列表 - {Title}",
            UserRelationType.Follower => $"关注者列表 - {Title}",
            UserRelationType.Blocked => $"绝交列表 - {Title}",
            _ => Title,
        };
        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPage);
        PageNavigator.JumpPage.InvokeCommand(LoadPageCommand);
        PageNavigator.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigator.PrevPage.InvokeCommand(LoadPageCommand);
    }

    [Reactive] public partial UserRelationType ListType { get; set; }

    public Task LoadPage(int? p, CancellationToken cancellationToken = default)
    {
        return ListType switch
        {
            UserRelationType.Friend => LoadFriend(p, cancellationToken),
            UserRelationType.Follower => LoadFollower(p, cancellationToken),
            UserRelationType.Blocked => LoadBlockList(cancellationToken),
            _ => throw new NotImplementedException(),
        };
    }

    public async Task LoadFriend(int? p, CancellationToken cancellationToken = default)
    {
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        FriendsGetResponse? response = null;
        try
        {
            response = (await ApiC.P1.Friends.GetAsFriendsGetResponseAsync(config =>
            {
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Limit = Limit;
            }, cancellationToken: cancellationToken));
        }
        catch (Exception e) { Trace.TraceError(e.Message); }

        SubjectViewModels = response?.Data?
            .Select<Friend, ViewModelBase>(f => new FriendViewModel(f) { RelationType = UserRelationType.Friend })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response?.Total);
    }
    public async Task LoadFollower(int? p, CancellationToken cancellationToken = default)
    {
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        FollowersGetResponse? response = null;
        try
        {
            response = (await ApiC.P1.Followers.GetAsFollowersGetResponseAsync(config =>
            {
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Limit = Limit;
            }, cancellationToken: cancellationToken));
        }
        catch (Exception e) { Trace.TraceError(e.Message); }

        SubjectViewModels = response?.Data?
            .Select<Friend, ViewModelBase>(f => new FriendViewModel(f) { RelationType = UserRelationType.Follower })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response?.Total);
    }
    public async Task LoadBlockList(CancellationToken cancellationToken = default)
    {
        List<int?>? blockList = null;
        try
        {
            blockList = (await ApiC.P1.Blocklist.GetAsBlocklistGetResponseAsync(cancellationToken: cancellationToken))?.Blocklist;
        }
        catch (Exception e) { Trace.TraceError(e.Message); }

        SubjectViewModels = blockList?
            .Select<int?, ViewModelBase>(uid => new UserViewModel(uid.ToString()) { Id = uid })
            .ToObservableCollection();
    }

    public static int Limit => CurrentSettings.SubjectBrowserPageSize;

    public ReactiveCommand<int?, Unit> LoadPageCommand { get; }
}
