using BangumiNet.Api.Interfaces;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class RevisionViewModel : ViewModelBase
{
    public RevisionViewModel(IRevision revision)
    {
        Id = revision.Id;
        Summary = revision.Summary;
        CreationTime = revision.CreatedAt;
        if (!string.IsNullOrWhiteSpace(revision.Creator?.Username))
            Creator = new(revision.Creator.Username) { Nickname = revision.Creator?.Nickname };
        Type = revision.Type;

        ShowUserCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new UserView() { DataContext = Creator } }.Show());
    }

    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? Type { get; set; }
    [Reactive] public partial UserViewModel? Creator { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }

    public ICommand? ShowUserCommand { get; set; }
}
