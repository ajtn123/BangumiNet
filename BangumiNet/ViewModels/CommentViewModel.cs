using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class CommentViewModel : ViewModelBase
{
    public CommentViewModel(CommentBase comment, CommentViewModel parent)
    {
        Parent = parent;
        ItemType = parent.ItemType;
        Content = comment.Content;
        CreatorId = comment.CreatorID;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = comment.State;
        if (comment.User != null)
            User = new(comment.User);
        if (comment.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct).ToLocalTime();
    }
    public CommentViewModel(Api.P1.P1.Characters.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Character;
        Content = comment.Content;
        CreatorId = comment.CreatorID;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        if (comment.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct).ToLocalTime();
    }
    public CommentViewModel(Api.P1.P1.Persons.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Person;
        Content = comment.Content;
        CreatorId = comment.CreatorID;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        if (comment.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct).ToLocalTime();
    }
    public CommentViewModel(Api.P1.P1.Episodes.Item.Comments.Comments comment)
    {
        ItemType = ItemType.Episode;
        Content = comment.Content;
        CreatorId = comment.CreatorID;
        MainId = comment.MainID;
        RelatedId = comment.RelatedID;
        Id = comment.Id;
        Reactions = new(comment.Reactions, Id, ItemType);
        State = comment.State;
        Replies = comment.Replies?.Select(r => new CommentViewModel(r, this)).ToObservableCollection();
        if (comment.User != null)
            User = new(comment.User);
        if (comment.CreatedAt is int ct)
            CreationTime = DateTimeOffset.FromUnixTimeSeconds(ct).ToLocalTime();
    }

    [Reactive] public partial ItemType ItemType { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial string? Content { get; set; }
    [Reactive] public partial int? CreatorId { get; set; }
    [Reactive] public partial int? MainId { get; set; }
    [Reactive] public partial int? RelatedId { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? State { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
    [Reactive] public partial ReactionListViewModel? Reactions { get; set; }
    [Reactive] public partial ObservableCollection<CommentViewModel>? Replies { get; set; }
    [Reactive] public partial CommentViewModel? Parent { get; set; }

    public bool NoReaction => (ItemType == ItemType.Character || ItemType == ItemType.Person) && Reactions?.Reactions == null;
}
