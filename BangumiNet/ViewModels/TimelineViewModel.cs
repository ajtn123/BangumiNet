using BangumiNet.Api.P1.Models;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class TimelineViewModel : SubjectListViewModel, ILoadable
{
    public TimelineViewModel()
    {
        LoadCommand = ReactiveCommand.CreateFromTask(async () => await Load(Until));
        LoadTopCommand = ReactiveCommand.CreateFromTask(async () => await Load(Until = 0));
        LoadNextCommand = ReactiveCommand.CreateFromTask(async () => await Load(Until = SubjectViewModels?.Select(x => (x as TimelineItemViewModel)?.Id ?? int.MaxValue).Min() ?? 0));
    }

    public Task Load(CancellationToken ct = default) => Load(Until, ct);
    public async Task Load(int until, CancellationToken cancellationToken = default)
    {
        int? u = until == 0 ? null : until;
        List<Timeline>? timelines = null;
        try
        {
            timelines = Username == null
                ? await ApiC.P1.Timeline.GetAsync(config =>
                {
                    config.QueryParameters.ModeAsFilterMode = OnlyFriend ? FilterMode.Friends : FilterMode.All;
                    config.QueryParameters.Until = u;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken)
                : await ApiC.P1.Users[Username].Timeline.GetAsync(config =>
                {
                    config.QueryParameters.Until = u;
                    config.QueryParameters.Limit = Limit;
                }, cancellationToken);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        if (timelines == null) return;

        SubjectViewModels = timelines.Select<Timeline, ViewModelBase>(t => new TimelineItemViewModel(t)).ToObservableCollection();
    }

    [Reactive] public partial bool OnlyFriend { get; set; }
    [Reactive] public partial int Until { get; set; }
    [Reactive] public partial string? Username { get; set; }

    public ICommand LoadCommand { get; set; }
    public ICommand LoadTopCommand { get; set; }
    public ICommand LoadNextCommand { get; set; }

    public static int Limit => 20;
}
