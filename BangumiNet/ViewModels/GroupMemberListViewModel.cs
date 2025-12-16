using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups.Item.Members;

namespace BangumiNet.ViewModels;

public partial class GroupMemberListViewModel : SubjectListPagedViewModel
{
    public GroupMemberListViewModel(string? groupname)
        => Groupname = groupname;

    protected override async Task LoadPageAsync(int? page, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(Groupname)) return;
        if (page is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;

        MembersGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups[Groupname].Members.GetAsync(config =>
            {
                config.Paging(Limit, offset);
                config.QueryParameters.Role = (int?)Role;
            }, cancellationToken: ct);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (response == null) return;

        SubjectViewModels = response.Data?
            .Select<GroupMember, ViewModelBase>(gm => new GroupMemberViewModel(gm))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial string? Groupname { get; set; }
    [Reactive] public partial GroupRole? Role { get; set; }

    public override int Limit => 20;
}
