using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class ReplyViewModel : ViewModelBase
{
    public ReplyViewModel(CommentViewModel? parent)
    {
        Content = string.Empty;
        Parent = parent;
        SendCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            _ = ApiC.P1.Characters[(int)parent.MainId].Comments.PostAsCommentsPostResponseAsync(new()
            {
                Content = Content,
                ReplyTo = parent.Id,
                TurnstileToken = await GetTurnstileInteraction.Handle(default).FirstAsync()
            });
        });
    }
    public ReplyViewModel(CommentListViewModel? ancestor)
    {
        Content = string.Empty;
        Ancestor = ancestor;
        SendCommand = ReactiveCommand.Create(() =>
        {

        });

    }

    [Reactive] public partial string Content { get; set; }
    [Reactive] public partial CommentViewModel? Parent { get; set; }
    [Reactive] public partial CommentListViewModel? Ancestor { get; set; }

    public ReactiveCommand<Unit, Unit> SendCommand { get; set; }
    public ReactiveCommand<Unit, Unit> DismissCommand { get; set; } = ReactiveCommand.Create(() => { });
    public Interaction<Unit, string> GetTurnstileInteraction { get; set; } = new();
}
