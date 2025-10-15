using System.Reactive;

namespace BangumiNet.ViewModels;

public partial class ReplyViewModel : ViewModelBase
{
    public ReplyViewModel(CommentViewModel? parent)
    {
        Content = string.Empty;
        Parent = parent;
        SendCommand = ReactiveCommand.Create(() =>
        {

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
}
