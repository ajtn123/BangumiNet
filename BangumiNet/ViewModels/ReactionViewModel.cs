using Avalonia.Media.Imaging;
using BangumiNet.Api.P1.Models;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class ReactionViewModel : ViewModelBase
{
    public ReactionViewModel(Reaction reaction)
    {
        Reaction = reaction.Value;
        Users = reaction.Users?.Select(u => new UserViewModel(u)).ToObservableCollection() ?? [];

        Init();
    }
    public ReactionViewModel(int? reaction)
    {
        Reaction = reaction;
        Users = [];

        Init();
    }

    public void Init()
    {
        ReactionImage = StickerProvider.GetStickerBitmap(Reaction);
        ReactCommand = ReactiveCommand.CreateFromTask(React);
        Users.CollectionChanged += (s, e) =>
        {
            this.RaisePropertyChanged(nameof(HasMe));
        };
    }

    [Reactive] public partial ReactionListViewModel? Parent { get; set; }
    [Reactive] public partial int? Reaction { get; set; }
    [Reactive] public partial Bitmap? ReactionImage { get; set; }
    [Reactive] public partial ObservableCollection<UserViewModel> Users { get; set; }

    public ICommand? ReactCommand { get; set; }

    public async Task React()
    {
        if (Parent?.CommentId is not int cid) return;
        if (!HasMe)
        {
            try
            {
                await ApiC.P1.Subjects.Minus.Collects[cid].Like.PutAsLikePutResponseAsync(new()
                {
                    Value = Reaction,
                });
                await Parent.UpdateMyReaction(Reaction);
            }
            catch (Exception e) { Trace.TraceError(e.Message); }
        }
        else
        {
            try
            {
                await ApiC.P1.Subjects.Minus.Collects[cid].Like.DeleteAsLikeDeleteResponseAsync();
                await Parent.UpdateMyReaction(null);
            }
            catch (Exception e) { Trace.TraceError(e.Message); }
        }
    }

    public bool HasMe => Users.Any(u => u.Username == ApiC.CurrentUsername);
}

public partial class ReactionListViewModel : ViewModelBase
{
    public ReactionListViewModel(List<Reaction>? reactions, int? commentId)
    {
        CommentId = commentId;
        Reactions = reactions?.Select(r => new ReactionViewModel(r) { Parent = this }).ToObservableCollection();
        ReactButtons = [.. ReactButtons.Select(r => { r.Parent = this; return r; })];
    }

    [Reactive] public partial ObservableCollection<ReactionViewModel>? Reactions { get; set; }
    [Reactive] public partial int? CommentId { get; set; }

    public ReactionViewModel[] ReactButtons { get; } = [
        new(0), new(104), new(54), new(140), new(122), new(90), new(88), new(80)
    ];

    public async Task UpdateMyReaction(int? sid)
    {
        if (sid == null)
        {
            if (Reactions == null) return;
            foreach (var reaction in Reactions)
                reaction.Users = reaction.Users.Where(u => !u.IsMe).ToObservableCollection();
            Reactions = Reactions.Where(r => r.Users.Any()).ToObservableCollection();
            if (!Reactions.Any()) Reactions = null;
        }
        else
        {
            var user = await ApiC.GetViewModelAsync<UserViewModel>();
            user ??= new UserViewModel(ApiC.CurrentUsername);
            Reactions ??= [];
            if (Reactions.FirstOrDefault(x => x!.Reaction == sid, null) is { } reaction)
            {
                reaction.Users.Add(user);
            }
            else
            {
                var newReaction = new ReactionViewModel(sid) { Parent = this };
                newReaction.Users.Add(user);
                Reactions.Add(newReaction);
            }
        }

        this.RaisePropertyChanged(nameof(HasMe));
    }

    public bool HasMe => Reactions?.Any(r => r.Users.Any(u => u.Username == ApiC.CurrentUsername)) ?? false;
}