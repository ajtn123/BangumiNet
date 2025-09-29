using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Shared.Interfaces;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class EpisodeViewModel : ViewModelBase, INeighboring
{
    public EpisodeViewModel(Episode episode)
    {
        Source = episode;
        Id = episode.Id;
        Type = (EpisodeType?)episode.Type;
        Name = episode.Name;
        NameCn = episode.NameCn;
        Sort = episode.Sort;
        Ep = episode.Ep;
        AirDate = Common.ParseBangumiDate(episode.Airdate);
        CommentCount = episode.Comment;
        DurationString = episode.Duration;
        Description = episode.Desc;
        Disc = episode.Disc;
        if (episode.DurationSeconds != null)
            Duration = TimeSpan.FromSeconds((long)episode.DurationSeconds);
        if (episode.AdditionalData.TryGetValue("subject_id", out var sid)) SubjectId = Common.NumberToInt(sid);

        Init();
    }
    public EpisodeViewModel(EpisodeDetail episode)
    {
        Source = episode;
        Id = episode.Id;
        Type = (EpisodeType?)episode.Type;
        Name = episode.Name;
        NameCn = episode.NameCn;
        Sort = episode.Sort;
        Ep = episode.Ep;
        AirDate = Common.ParseBangumiDate(episode.Airdate);
        CommentCount = episode.Comment;
        DurationString = episode.Duration;
        Description = episode.Desc;
        Disc = episode.Disc;
        SubjectId = episode.SubjectId;
        if (episode.AdditionalData.TryGetValue("duration_seconds", out var ds) && Common.NumberToInt(ds) is int t)
            Duration = TimeSpan.FromSeconds(t);

        Init();
    }

    public void Init()
    {
        if (Duration?.TotalSeconds == 0) Duration = null;
        if (Disc == 0) Disc = null;
        if (Ep == 0) Ep = null;

        this.WhenAnyValue(x => x.DurationString, x => x.Duration).Subscribe(x => this.RaisePropertyChanged(nameof(ShouldDisplayDurationString)));

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new EpisodeView() { DataContext = this } }.Show());
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvEpisodeUrlBase + Id));
        ShowPrevCommand = ReactiveCommand.Create(() => Prev, this.WhenAnyValue(x => x.Prev).Select(y => y != null));
        ShowNextCommand = ReactiveCommand.Create(() => Next, this.WhenAnyValue(x => x.Next).Select(y => y != null));
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? SubjectId { get; set; }
    [Reactive] public partial EpisodeType? Type { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial double? Sort { get; set; }
    [Reactive] public partial double? Ep { get; set; }
    [Reactive] public partial DateOnly? AirDate { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }
    [Reactive] public partial string? DurationString { get; set; }
    [Reactive] public partial string? Description { get; set; }
    [Reactive] public partial int? Disc { get; set; }
    [Reactive] public partial TimeSpan? Duration { get; set; }

    [Reactive] public partial INeighboring? Prev { get; set; }
    [Reactive] public partial INeighboring? Next { get; set; }

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchWebCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }
    public ReactiveCommand<Unit, INeighboring?>? ShowPrevCommand { get; private set; }
    public ReactiveCommand<Unit, INeighboring?>? ShowNextCommand { get; private set; }

    public bool ShouldDisplayDurationString => Duration == null && !string.IsNullOrWhiteSpace(DurationString);
}
