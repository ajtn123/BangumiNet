using BangumiNet.Api.Helpers;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class GroupListViewModel : SubjectListPagedViewModel
{
    public GroupListViewModel()
    {
        Filter = GroupFilterMode.All;
        Sort = GroupSort.Posts;

        this.WhenAnyValue(x => x.Filter).Skip(1).Subscribe(f =>
        {
            _ = LoadPageCommand.Execute(1).Subscribe();
        });
        this.WhenAnyValue(x => x.Sort).Skip(1).Subscribe(f =>
        {
            _ = LoadPageCommand.Execute(1).Subscribe();
        });
    }

    protected override async Task LoadPageAsync(int? page, CancellationToken ct = default)
    {
        if (page is not int p) return;
        int offset = (p - 1) * Limit;

        GroupsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups.GetAsync(config =>
            {
                config.Paging(Limit, offset);
                config.QueryParameters.Mode = Filter;
                config.QueryParameters.Sort = Sort;
            }, cancellationToken: ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?
            .Select<SlimGroup, ViewModelBase>(t => new GroupViewModel(t))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial GroupFilterMode Filter { get; set; }
    [Reactive] public partial GroupSort Sort { get; set; }

    public override int Limit => 20;
}
