using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Followers;
using BangumiNet.Api.P1.P1.Friends;

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
    }

    [Reactive] public partial UserRelationType ListType { get; set; }

    protected override Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        return ListType switch
        {
            UserRelationType.Friend => LoadFriend(page, cancellationToken),
            UserRelationType.Follower => LoadFollower(page, cancellationToken),
            UserRelationType.Blocked => LoadBlockList(cancellationToken),
            _ => throw new NotImplementedException(),
        };
    }

    private async Task LoadFriend(int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;
        int offset = (p - 1) * Limit;

        FriendsGetResponse? response = null;
        try
        {
            response = (await ApiC.P1.Friends.GetAsync(config =>
            {
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Limit = Limit;
            }, cancellationToken: cancellationToken));
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?
            .Select<Friend, ViewModelBase>(f => new FriendViewModel(f) { RelationType = UserRelationType.Friend })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response?.Total);
    }
    private async Task LoadFollower(int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;
        int offset = (p - 1) * Limit;

        FollowersGetResponse? response = null;
        try
        {
            response = (await ApiC.P1.Followers.GetAsync(config =>
            {
                config.QueryParameters.Offset = offset;
                config.QueryParameters.Limit = Limit;
            }, cancellationToken: cancellationToken));
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?
            .Select<Friend, ViewModelBase>(f => new FriendViewModel(f) { RelationType = UserRelationType.Follower })
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response?.Total);
    }
    private async Task LoadBlockList(CancellationToken cancellationToken = default)
    {
        List<int?>? blockList = null;
        try
        {
            blockList = (await ApiC.P1.Blocklist.GetAsync(cancellationToken: cancellationToken))?.Blocklist;
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (blockList == null) return;

        SubjectViewModels = blockList?
            .Select<int?, ViewModelBase>(uid => new UserViewModel(uid.ToString()) { Id = uid })
            .ToObservableCollection();
    }

    public override int Limit => Settings.SubjectBrowserPageSize;
}
