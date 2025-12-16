using System.Reactive;

namespace BangumiNet.ViewModels;

public abstract partial class SubjectListPagedViewModel : SubjectListViewModel
{
    public SubjectListPagedViewModel()
    {
        LoadPageCommand = ReactiveCommand.CreateFromTask<int?>(LoadPageAsync);

        PageNavigator.PrevPage.InvokeCommand(LoadPageCommand);
        PageNavigator.NextPage.InvokeCommand(LoadPageCommand);
        PageNavigator.JumpPage.InvokeCommand(LoadPageCommand);
    }

    public abstract int Limit { get; }
    protected abstract Task LoadPageAsync(int? page, CancellationToken cancellationToken = default);
    public ReactiveCommand<int?, Unit> LoadPageCommand { get; private init; }
    public PageNavigatorViewModel PageNavigator { get; } = new();
}
