using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class GroupMemberViewModel : UserViewModel
{
    public GroupMemberViewModel(GroupMember user) : base(user.User ?? new())
    {
        Id = user.Uid;
        Role = (GroupRole?)user.Role;
        JoinTime = Common.ParseBangumiTime(user.JoinedAt);
    }

    [Reactive] public partial GroupRole? Role { get; set; }
    [Reactive] public partial DateTimeOffset? JoinTime { get; set; }
}
