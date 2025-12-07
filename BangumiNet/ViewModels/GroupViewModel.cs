using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class GroupViewModel : ItemViewModelBase
{
    public GroupViewModel(SlimGroup group)
    {
        Source = group;
        Id = group.Id;
        Name = group.Title;
        Groupname = group.Name;
        Images = group.Icon;
        IsNsfw = group.Nsfw ?? false;
        Accessible = group.Accessible ?? true;
        MemberCount = group.Members;
        CreationTime = CommonUtils.ParseBangumiTime(group.CreatedAt);

        Init();
    }
    public GroupViewModel(Group group)
    {
        Source = group;
        Id = group.Id;
        Name = group.Title;
        Groupname = group.Name;
        Images = group.Icon;
        IsNsfw = group.Nsfw ?? false;
        Accessible = group.Accessible ?? true;
        MemberCount = group.Members;
        CreationTime = CommonUtils.ParseBangumiTime(group.CreatedAt);
        TopicCount = group.Topics;
        Category = group.Cat;
        Description = group.Description;
        PostCount = group.Posts;
        if (group.Creator != null)
            Creator = new(group.Creator) { Id = group.CreatorID, Role = Api.ExtraEnums.GroupRole.Leader, JoinTime = CreationTime };
        if (group.Membership != null)
            Membership = new(group.Membership);

        Init();
    }
    public void Init()
    {
        ItemType = ItemType.Group;
        Title = $"{Name} - {Title}";
        SearchWebCommand = ReactiveCommand.Create(() => CommonUtils.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(UrlProvider.BangumiTvGroupUrlBase + Groupname));
        Members = new(Groupname);
        Topics = new(Groupname);
    }

    [Reactive] public partial string? Groupname { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial bool Accessible { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial int? MemberCount { get; set; }
    [Reactive] public partial int? TopicCount { get; set; }
    [Reactive] public partial int? PostCount { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial GroupMemberViewModel? Creator { get; set; }
    [Reactive] public partial GroupMemberViewModel? Membership { get; set; }
    [Reactive] public partial string? Description { get; set; }
    [Reactive] public partial int? Category { get; set; }
    [Reactive] public partial GroupMemberListViewModel? Members { get; set; }
    [Reactive] public partial GroupTopicListViewModel? Topics { get; set; }

    public bool IsFull => Source is Group;
}
