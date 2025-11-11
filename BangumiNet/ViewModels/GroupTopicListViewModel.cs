using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups.Item.Topics;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class GroupTopicListViewModel : SubjectListPagedViewModel
{
    public GroupTopicListViewModel(string? groupname)
    {
        Groupname = groupname;

        LoadCommand = ReactiveCommand.CreateFromTask<int?>(Load);

        PageNavigator.PrevPage.InvokeCommand(LoadCommand);
        PageNavigator.NextPage.InvokeCommand(LoadCommand);
        PageNavigator.JumpPage.InvokeCommand(LoadCommand);
    }
    public async Task Load(int? p, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(Groupname)) return;
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        TopicsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups[Groupname].Topics.GetAsTopicsGetResponseAsync(config =>
            {
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
            .Select<Topic, ViewModelBase>(t => new TopicViewModel(t, ItemType.Group))
            .ToObservableCollection();
        PageNavigator.UpdatePageInfo(Limit, offset, response.Total);
    }

    [Reactive] public partial string? Groupname { get; set; }

    public ICommand LoadCommand { get; set; }

    public static int Limit => 20;
}
