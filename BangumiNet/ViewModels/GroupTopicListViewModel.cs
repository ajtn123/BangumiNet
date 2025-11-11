using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Groups.Item.Topics;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class GroupTopicListViewModel : SubjectListPagedViewModel
{
    public GroupTopicListViewModel(string? groupname)
    {
        Groupname = groupname;
        BriefFilter = GroupTopicFilterMode.All;

        LoadCommand = ReactiveCommand.CreateFromTask<int?>(Load);

        PageNavigator.PrevPage.InvokeCommand(LoadCommand);
        PageNavigator.NextPage.InvokeCommand(LoadCommand);
        PageNavigator.JumpPage.InvokeCommand(LoadCommand);

        this.WhenAnyValue(x => x.BriefFilter).Skip(1).Subscribe(f =>
        {
            if (Groupname == null)
                _ = Load(1);
        });
    }
    public Task Load(int? p, CancellationToken ct = default)
        => Groupname == null ? LoadBrief(p, ct) : LoadParticle(p, ct);

    private async Task LoadParticle(int? p, CancellationToken ct = default)
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

    private async Task LoadBrief(int? p, CancellationToken ct = default)
    {
        if (p is not int pageIndex) return;
        int offset = (pageIndex - 1) * Limit;
        Api.P1.P1.Groups.Topics.TopicsGetResponse? response = null;
        try
        {
            response = await ApiC.P1.Groups.Minus.Topics.GetAsTopicsGetResponseAsync(config =>
            {
                config.QueryParameters.Limit = Limit;
                config.QueryParameters.Offset = offset;
                config.QueryParameters.ModeAsGroupTopicFilterMode = BriefFilter;
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
    [Reactive] public partial GroupTopicFilterMode BriefFilter { get; set; }

    public ICommand LoadCommand { get; set; }

    public static int Limit => 20;
}
