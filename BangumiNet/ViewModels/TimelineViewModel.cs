using BangumiNet.Api.Misc;
using BangumiNet.Api.P1.Models;
using System.Reactive;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class TimelineViewModel : SubjectListViewModel, IActivatableViewModel
{
    public TimelineViewModel()
    {
        this.WhenActivated(disposables =>
        {
            LoadCommand = ReactiveCommand.CreateFromTask(async ct => await LoadAsync(Until, ct)).DisposeWith(disposables);
            LoadTopCommand = ReactiveCommand.CreateFromTask(async ct => await LoadAsync(Until = 0, ct)).DisposeWith(disposables);
            LoadNextCommand = ReactiveCommand.CreateFromTask(async ct =>
            {
                var ids = SubjectViewModels?.Select(t => (t as TimelineItemViewModel)?.Id).OfType<int>().ToArray();
                if (ids == null || ids.Length == 0) return;
                await LoadAsync(Until = ids.Min(), ct);
            }).DisposeWith(disposables);

            this.WhenAnyValue(x => x.Mode).Skip(1).Subscribe(async x =>
            {
                if (Username != null) return;

                if (IsEventStreaming) _ = Connect();
                else _ = LoadTopCommand.Execute().Subscribe();
            }).DisposeWith(disposables);

            this.WhenAnyValue(x => x.IsEventStreaming).Skip(1).Subscribe(x =>
            {
                if (IsEventStreaming) _ = Connect();
                else Disconnect();
            }).DisposeWith(disposables);

            if (SubjectViewModels == null) LoadCommand.Execute().Subscribe();
        });
    }

    private async Task LoadAsync(int until, CancellationToken cancellationToken = default)
    {
        IsEventStreaming = false;

        int? u = until == 0 ? null : until;
        List<Timeline>? timelines = null;
        try
        {
            timelines = Username == null
                ? await ApiC.P1.Timeline.GetAsync(config =>
                {
                    config.QueryParameters.Mode = Mode;
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

    private readonly TimelineEventStream events = new(ApiC.HttpClient, Settings.AuthToken);
    private CancellationTokenSource cts = new();
    private async Task Connect()
    {
        Disconnect();

        try
        {
            SubjectViewModels = [];
            await foreach (var item in events.StartAsync(Mode, null, cts.Token))
            {
                if (SubjectViewModels.Count >= 100)
                    SubjectViewModels.RemoveAt(99);
                SubjectViewModels.Insert(0, new TimelineItemViewModel(item));
            }
        }
        catch (TaskCanceledException) { Trace.WriteLine("Timeline SSE connection ended as requested."); }
        catch (Exception e) { Trace.WriteLine(e.Message); }
    }
    private void Disconnect()
    {
        cts.Cancel();
        cts.Dispose();
        cts = new();
    }

    [Reactive] public partial bool IsEventStreaming { get; set; }
    [Reactive] public partial FilterMode Mode { get; set; }
    [Reactive] public partial int Until { get; set; }
    [Reactive] public partial string? Username { get; set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit>? LoadCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? LoadTopCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? LoadNextCommand { get; set; }

    public static int Limit => 20;
    public ViewModelActivator Activator { get; } = new();
}
