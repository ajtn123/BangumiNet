using Avalonia.Media.Imaging;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class ReactionViewModel : ViewModelBase
{
    public ReactionViewModel(Reaction reaction)
    {
        Reaction = reaction.Value;
        ReactionImage = StickerProvider.GetStickerBitmap(reaction.Value);
        Users = reaction.Users?.Select(u => new UserViewModel(u)).ToObservableCollection();
        Users?.CollectionChanged += (s, e) =>
        {
            this.RaisePropertyChanged(nameof(HasMe));
        };
    }

    [Reactive] public partial int? Reaction { get; set; }
    [Reactive] public partial Bitmap? ReactionImage { get; set; }
    [Reactive] public partial ObservableCollection<UserViewModel>? Users { get; set; }

    public bool HasMe => Users?.Any(u => u.Username == ApiC.CurrentUsername) ?? false;
}

public partial class ReactionListViewModel : ViewModelBase
{
    public ReactionListViewModel(List<Reaction>? reactions, int? commentId)
    {
        Reactions = reactions?.Select(r => new ReactionViewModel(r)).ToObservableCollection();
        CommentId = commentId;
    }

    [Reactive] public partial ObservableCollection<ReactionViewModel>? Reactions { get; set; }
    [Reactive] public partial int? CommentId { get; set; }
}
