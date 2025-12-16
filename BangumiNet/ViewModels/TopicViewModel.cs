using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;

namespace BangumiNet.ViewModels;

public partial class TopicViewModel : ItemViewModelBase
{
    public TopicViewModel(SubjectTopic topic, bool isFull)
    {
        IsFull = isFull;
        ParentType = ItemType.Subject;
        CreationTime = CommonUtils.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(topic.UpdatedAt);
        State = (CommentState?)topic.State;
        Display = (TopicDisplay?)topic.Display;
        Name = topic.Title;
        Id = topic.Id;
        ParentId = topic.ParentID;
        ReplyCount = topic.ReplyCount;
        Replies = new(ItemType.Topic, Id) { ParentItemType = ParentType };
        if (topic.Replies != null)
            Replies.LoadReplies(topic.Replies);
        if (topic.Creator != null)
            User = new(topic.Creator) { Id = topic.CreatorID };
        if (topic.Subject != null)
            Parent = new SubjectViewModel(topic.Subject);
    }
    public TopicViewModel(GroupTopic topic, bool isFull)
    {
        IsFull = isFull;
        ParentType = ItemType.Group;
        CreationTime = CommonUtils.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(topic.UpdatedAt);
        State = (CommentState?)topic.State;
        Display = (TopicDisplay?)topic.Display;
        Name = topic.Title;
        Id = topic.Id;
        ParentId = topic.ParentID;
        ReplyCount = topic.ReplyCount;
        Replies = new(ItemType.Topic, Id) { ParentItemType = ParentType };
        if (topic.Replies != null)
            Replies.LoadReplies(topic.Replies);
        if (topic.Creator != null)
            User = new(topic.Creator) { Id = topic.CreatorID };
        if (topic.Group != null)
            Parent = new GroupViewModel(topic.Group);
    }
    public TopicViewModel(Topic topic, ItemType parentType)
    {
        IsFull = false;
        ParentType = parentType;
        CreationTime = CommonUtils.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = CommonUtils.ParseBangumiTime(topic.UpdatedAt);
        State = (CommentState?)topic.State;
        Display = (TopicDisplay?)topic.Display;
        Name = topic.Title;
        Id = topic.Id;
        ParentId = topic.ParentID;
        ReplyCount = topic.ReplyCount;
        Replies = new(ItemType.Topic, Id) { ParentItemType = ParentType };
        if (topic.Creator != null)
            User = new(topic.Creator) { Id = topic.CreatorID };
    }

    protected override void Activate(CompositeDisposable disposables)
    {
        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(ParentType switch
        {
            ItemType.Subject => UrlProvider.BangumiTvSubjectTopicUrlBase + Id,
            ItemType.Group => UrlProvider.BangumiTvGroupTopicUrlBase + Id,
            _ => throw new NotImplementedException()
        })).DisposeWith(disposables);
    }

    [Reactive] public partial ItemType? ParentType { get; set; }
    [Reactive] public partial ItemViewModelBase? Parent { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial CommentState? State { get; set; }
    [Reactive] public partial TopicDisplay? Display { get; set; }
    [Reactive] public partial int? ParentId { get; set; }
    [Reactive] public partial int? ReplyCount { get; set; }
    [Reactive] public partial CommentListViewModel? Replies { get; set; }
    [Reactive] public partial bool IsFull { get; set; }

    public bool IsDisplayed => Display == TopicDisplay.Normal;
    public override ItemType ItemType { get; init; } = ItemType.Topic;
}
