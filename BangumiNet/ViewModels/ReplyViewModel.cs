using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class ReplyViewModel : ViewModelBase
{
    public ReplyViewModel(CommentViewModel? parent)
    {
        Content = string.Empty;
        Parent = parent;
        SendCommand = ReactiveCommand.CreateFromTask(async _ =>
        {
            if (Parent?.MainId == null || Parent.Id == null || string.IsNullOrWhiteSpace(Content)) return false;
            string token = await GetTurnstileInteraction.Handle(default);
            if (token == null) return false;
            try
            {
                int? ncid = null;
                if (Parent.ItemType == ItemType.Person)
                    ncid = (await ApiC.P1.Persons[(int)Parent.MainId].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = Parent.Id,
                        TurnstileToken = token
                    }))?.Id;
                else if (Parent.ItemType == ItemType.Character)
                    ncid = (await ApiC.P1.Characters[(int)Parent.MainId].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = Parent.Id,
                        TurnstileToken = token
                    }))?.Id;
                else if (Parent.ItemType == ItemType.Episode)
                    ncid = (await ApiC.P1.Episodes[(int)Parent.MainId].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = Parent.Id,
                        TurnstileToken = token
                    }))?.Id;
                else throw new NotImplementedException();

                Parent.Replies ??= [];
                Parent.Replies.Add(new()
                {
                    Id = ncid,
                    CreationTime = DateTimeOffset.Now.ToLocalTime(),
                    Content = Content,
                    MainId = Parent.MainId,
                    ItemType = Parent.ItemType,
                    Parent = Parent,
                    User = await ApiC.GetViewModelAsync<UserViewModel>(),
                });
                return true;
            }
            catch (Exception e) { Trace.TraceError(e.Message); }
            return false;
        });
    }
    public ReplyViewModel(CommentListViewModel? ancestor)
    {
        Content = string.Empty;
        Ancestor = ancestor;
        SendCommand = ReactiveCommand.CreateFromTask(async _ =>
        {
            if (Ancestor?.Id == null || string.IsNullOrWhiteSpace(Content)) return false;
            string token = await GetTurnstileInteraction.Handle(default);
            if (token == null) return false;
            try
            {
                int? ncid = null;
                if (Ancestor.ItemType == ItemType.Person)
                    ncid = (await ApiC.P1.Persons[(int)Ancestor.Id].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = 0,
                        TurnstileToken = token
                    }))?.Id;
                else if (Ancestor.ItemType == ItemType.Character)
                    ncid = (await ApiC.P1.Characters[(int)Ancestor.Id].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = 0,
                        TurnstileToken = token
                    }))?.Id;
                else if (Ancestor.ItemType == ItemType.Episode)
                    ncid = (await ApiC.P1.Episodes[(int)Ancestor.Id].Comments.PostAsCommentsPostResponseAsync(new()
                    {
                        Content = Content,
                        ReplyTo = 0,
                        TurnstileToken = token
                    }))?.Id;
                else throw new NotImplementedException();

                Ancestor.Comments ??= [];
                Ancestor.Comments.Add(new CommentViewModel()
                {
                    Id = ncid,
                    CreationTime = DateTimeOffset.Now.ToLocalTime(),
                    Content = Content,
                    MainId = Ancestor.Id,
                    ItemType = (ItemType)Ancestor.ItemType,
                    User = await ApiC.GetViewModelAsync<UserViewModel>(),
                });
                return true;
            }
            catch (Exception e) { Trace.TraceError(e.Message); }
            return false;
        });
    }

    [Reactive] public partial string Content { get; set; }
    [Reactive] public partial CommentViewModel? Parent { get; set; }
    [Reactive] public partial CommentListViewModel? Ancestor { get; set; }

    public ReactiveCommand<Unit, bool> SendCommand { get; set; }
    public ReactiveCommand<Unit, bool> DismissCommand { get; set; } = ReactiveCommand.Create(() => true);
    public Interaction<Unit, string?> GetTurnstileInteraction { get; set; } = new();
}
