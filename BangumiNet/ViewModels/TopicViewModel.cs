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
        Replies = new(ItemType.Topic, Id);
        if (topic.Replies != null)
            Replies.LoadReplies(topic.Replies);
        if (topic.Creator != null)
            User = new(topic.Creator) { Id = topic.CreatorID };
        if (topic.Subject != null)
            Subject = new(topic.Subject);

        Init();
    }

    public void Init()
    {
        ItemTypeEnum = ItemType.Topic;
        Title = $"{Name} - {Title}";
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(ParentType switch
        {
            ItemType.Subject => UrlProvider.BangumiTvSubjectTopicUrlBase + Id,
            //ItemType.Group => UrlProvider.BangumiTvGroupTopicUrlBase + Id,
            _ => throw new NotImplementedException()
        }));
    }

    [Reactive] public partial ItemType? ParentType { get; set; }
    [Reactive] public partial SubjectViewModel? Subject { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial DateTimeOffset? UpdateTime { get; set; }
    [Reactive] public partial CommentState? State { get; set; }
    [Reactive] public partial TopicDisplay? Display { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial int? ParentId { get; set; }
    [Reactive] public partial int? ReplyCount { get; set; }
    [Reactive] public partial CommentListViewModel? Replies { get; set; }
    [Reactive] public partial bool IsFull { get; set; }
}
