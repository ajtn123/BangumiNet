using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class FriendViewModel : ViewModelBase
{
    public FriendViewModel(Friend friend)
    {
        if (friend.User != null)
            User = new(friend.User);
        CreationTime = CommonUtils.ParseBangumiTime(friend.CreatedAt);
        Grade = friend.Grade;
        Description = friend.Description;
    }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial string? Description { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial int? Grade { get; set; }
    [Reactive] public partial UserRelationType? RelationType { get; set; }
}
public enum UserRelationType
{
    Friend,
    Follower,
    Blocked,
}