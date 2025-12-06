using BangumiNet.Api.P1.Models;
using BangumiNet.Shared.Interfaces;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class TimelineViewModel : SubjectListViewModel, ILoadable
{
    public TimelineViewModel()
    {
        LoadCommand = ReactiveCommand.CreateFromTask(async ct => await Load(Until, ct));
        LoadTopCommand = ReactiveCommand.CreateFromTask(async ct => await Load(Until = 0, ct));
        LoadNextCommand = ReactiveCommand.CreateFromTask(async ct =>
        {
            var ids = SubjectViewModels?.Select(t => (t as TimelineItemViewModel)?.Id).OfType<int>().ToArray();
            if (ids == null || ids.Length == 0) return;
            await Load(Until = ids.Min(), ct);
        });

        this.WhenAnyValue(x => x.OnlyFriend).Skip(1).Subscribe(async x =>
        {
            if (Username != null) return;
            await LoadTopCommand.Execute();
        });
    }

    public async Task Load(CancellationToken ct = default) => await LoadCommand.Execute();
    public async Task Load(int until, CancellationToken cancellationToken = default)
    {
        int? u = until == 0 ? null : until;
        List<Timeline>? timelines = null;
        try
        {
            timelines = Username == null
                ? await ApiC.P1.Timeline.GetAsync(config =>
                {
                    config.QueryParameters.Mode = OnlyFriend ? FilterMode.Friends : FilterMode.All;
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

    public ReactiveCommand<Unit, Unit> LoadCommand { get; set; }
    public ReactiveCommand<Unit, Unit> LoadTopCommand { get; set; }
    public ReactiveCommand<Unit, Unit> LoadNextCommand { get; set; }

    public static int Limit => 20;
}
