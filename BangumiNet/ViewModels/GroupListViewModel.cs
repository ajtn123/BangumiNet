using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class GroupListViewModel : SubjectListPagedViewModel
{
    public GroupListViewModel()
    {
        Filter = GroupFilterMode.All;
        Sort = GroupSort.Posts;

        LoadCommand = ReactiveCommand.CreateFromTask<int?>(Load);

        PageNavigator.PrevPage.InvokeCommand(LoadCommand);
        PageNavigator.NextPage.InvokeCommand(LoadCommand);
        PageNavigator.JumpPage.InvokeCommand(LoadCommand);

        this.WhenAnyValue(x => x.Filter).Skip(1).Subscribe(f =>
        {
            _ = Load(1);
        });
        this.WhenAnyValue(x => x.Sort).Skip(1).Subscribe(f =>
        {
            _ = Load(1);
        });
    }

    public async Task Load(int? p, CancellationToken ct = default)
    {
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        GroupsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups.GetAsGroupsGetResponseAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
                config.QueryParameters.ModeAsGroupFilterMode = Filter;
                config.QueryParameters.SortAsGroupSort = Sort;
            }, cancellationToken: ct);
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (response == null) return;
        SubjectViewModels = response.Data?
            .Select<SlimGroup, ViewModelBase>(t => new GroupViewModel(t))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial GroupFilterMode Filter { get; set; }
    [Reactive] public partial GroupSort Sort { get; set; }

    public ICommand LoadCommand { get; set; }

    public static int Limit => 20;
}
