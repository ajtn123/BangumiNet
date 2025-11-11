using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups.Item.Members;

namespace BangumiNet.ViewModels;

public partial class GroupMemberListView : SubjectListPagedViewModel
{
    public async Task Load(int? p, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(Groupname)) return;
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        MembersGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups[Groupname].Members.GetAsMembersGetResponseAsync(config =>
            {
                config.QueryParameters.Role = (int?)Role;
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
            }, cancellationToken: ct);
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            return;
        }
        if (response == null) return;
        SubjectViewModels = response.Data?
            .Select<GroupMember, ViewModelBase>(gm => new GroupMemberViewModel(gm))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial string? Groupname { get; set; }
    [Reactive] public partial GroupRole? Role { get; set; }

    public static int Limit => 20;
}
