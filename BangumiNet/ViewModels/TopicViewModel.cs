using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class TopicViewModel : ItemViewModelBase
{
    public TopicViewModel(SubjectTopic topic, bool isFull)
    {
        IsFull = isFull;
        ParentType = ItemType.Subject;
        CreationTime = Common.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = Common.ParseBangumiTime(topic.UpdatedAt);
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

        Init();
    }
    public TopicViewModel(GroupTopic topic, bool isFull)
    {
        IsFull = isFull;
        ParentType = ItemType.Group;
        CreationTime = Common.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = Common.ParseBangumiTime(topic.UpdatedAt);
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

        Init();
    }
    public TopicViewModel(Topic topic, ItemType parentType)
    {
        IsFull = false;
        ParentType = parentType;
        CreationTime = Common.ParseBangumiTime(topic.CreatedAt);
        UpdateTime = Common.ParseBangumiTime(topic.UpdatedAt);
        State = (CommentState?)topic.State;
        Display = (TopicDisplay?)topic.Display;
        Name = topic.Title;
        Id = topic.Id;
        ParentId = topic.ParentID;
        ReplyCount = topic.ReplyCount;
        Replies = new(ItemType.Topic, Id) { ParentItemType = ParentType };
        if (topic.Creator != null)
            User = new(topic.Creator) { Id = topic.CreatorID };

        Init();
    }

    public void Init()
    {
        ItemType = ItemType.Topic;
        Title = $"{Name} - {Title}";
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(ParentType switch
        {
            ItemType.Subject => UrlProvider.BangumiTvSubjectTopicUrlBase + Id,
            ItemType.Group => UrlProvider.BangumiTvGroupTopicUrlBase + Id,
            _ => throw new NotImplementedException()
        }));
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
}
