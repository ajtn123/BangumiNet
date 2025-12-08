using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class CommentViewModel : ViewModelBase
{
    public CommentViewModel() { }
    public CommentViewModel(CommentBase comment, CommentViewModel parent)
    {
        Parent = parent;
        ItemType = parent.ItemType;
        Content = comment.Content;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = (CommentState?)comment.State;
        if (comment.User != null)
            User = new(comment.User);
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Api.P1.P1.Characters.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Character;
        Content = comment.Content;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = (CommentState?)comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Api.P1.P1.Persons.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Person;
        Content = comment.Content;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = (CommentState?)comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Api.P1.P1.Episodes.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Episode;
        Content = comment.Content;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = (CommentState?)comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Api.P1.P1.Blogs.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Episode;
        Content = comment.Content;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = (CommentState?)comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Reply comment, ItemType itemType, int? mainId, ItemType? parentItemType = null)
    {
        ItemType = itemType;
        ParentItemType = parentItemType;
        Content = comment.Content;
        Id = comment.Id;
        MainId = mainId;
        Reactions = new(comment.Reactions, Id, ItemType) { ParentItemType = ParentItemType };
        State = (CommentState?)comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.Creator != null)
            User = new(comment.Creator) { Id = comment.CreatorID };
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(ReplyBase comment, CommentViewModel parent)
    {
        Parent = parent;
        ItemType = parent.ItemType;
        ParentItemType = parent.ParentItemType;
        Content = comment.Content;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType) { ParentItemType = ParentItemType };
        State = (CommentState?)comment.State;
        if (comment.Creator != null)
            User = new(comment.Creator) { Id = comment.CreatorID };
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
    }
    public CommentViewModel(Api.P1.P1.Timeline.Item.Replies.Replies comment)
    {
        ItemType = ItemType.Timeline;
        Content = comment.Content;
        Id = comment.Id;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Reactions = new(comment.Reactions, Id, ItemType) { ParentItemType = ParentItemType };
        State = (CommentState?)comment.State;
        CreationTime = CommonUtils.ParseBangumiTime(comment.CreatedAt);
        Replies = comment.RepliesProp?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User) { Id = comment.CreatorID };
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial ItemType? ParentItemType { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial string? Content { get; set; }
    [Reactive] public partial int? MainId { get; set; }
    [Reactive] public partial int? RelatedId { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial CommentState? State { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial ReactionListViewModel? Reactions { get; set; }
    [Reactive] public partial ObservableCollection<CommentViewModel>? Replies { get; set; }
    [Reactive] public partial CommentViewModel? Parent { get; set; }

    private static readonly ItemType[] NoReactionItemTypes = [ItemType.Character, ItemType.Person, ItemType.Blog, ItemType.Timeline];
    private static readonly ItemType[] NoReplyItemTypes = [ItemType.Subject];
    public bool NoReaction => NoReactionItemTypes.Contains(ItemType) && Reactions?.Reactions == null;
    public bool NoReply => NoReplyItemTypes.Contains(ItemType) && Replies == null;
}
