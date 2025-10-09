using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using BangumiNet.Shared.Interfaces;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class EpisodeViewModel : ItemViewModelBase, INeighboring
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

    public static EpisodeViewModel InitFormCollection(UserEpisodeCollection collection)
        => new(collection.Episode ?? new())
        {
            Status = (EpisodeCollectionType?)collection.Type,
            StatusUpdateTime = (collection.UpdatedAt == null || collection.UpdatedAt == 0) ? null : DateTimeOffset.FromUnixTimeSeconds((long)collection.UpdatedAt).ToLocalTime(),
        };

    public void Init()
    {
        RevisionListViewModel = new(ItemType.Episode, Id);

        if (string.IsNullOrWhiteSpace(Name)) Name = null;
        if (string.IsNullOrWhiteSpace(NameCn)) NameCn = null;
        if (Duration?.TotalSeconds == 0) Duration = null;
        if (Disc == 0) Disc = null;
        if (Ep == 0) Ep = null;

        this.WhenAnyValue(x => x.DurationString, x => x.Duration).Subscribe(x => this.RaisePropertyChanged(nameof(ShouldDisplayDurationString)));

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new EpisodeView() { DataContext = this } }.Show());
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvEpisodeUrlBase + Id));
        ShowPrevCommand = ReactiveCommand.Create(() => Prev, this.WhenAnyValue(x => x.Prev).Select(y => y != null));
        ShowNextCommand = ReactiveCommand.Create(() => Next, this.WhenAnyValue(x => x.Next).Select(y => y != null));
        UncollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateStatus(EpisodeCollectionType.Uncollected), this.WhenAnyValue(x => x.Status).Select(y => y != EpisodeCollectionType.Uncollected));
        WishCommand = ReactiveCommand.CreateFromTask(async () => await UpdateStatus(EpisodeCollectionType.Wish), this.WhenAnyValue(x => x.Status).Select(y => y != EpisodeCollectionType.Wish));
        DoneCommand = ReactiveCommand.CreateFromTask(async () => await UpdateStatus(EpisodeCollectionType.Done), this.WhenAnyValue(x => x.Status).Select(y => y != EpisodeCollectionType.Done));
        DropCommand = ReactiveCommand.CreateFromTask(async () => await UpdateStatus(EpisodeCollectionType.Dropped), this.WhenAnyValue(x => x.Status).Select(y => y != EpisodeCollectionType.Dropped));
        DoneUntilCommand = ReactiveCommand.CreateFromTask(async () => await UpdateStatusUntilThis(EpisodeCollectionType.Done), this.WhenAnyValue(x => x.Parent, x => x.Ep, x => x.Status).Select(y => y.Item1 != null && y.Item2 != null && y.Item3 != EpisodeCollectionType.Done));

        Title = $"{NameCnCvt.Convert(this) ?? $"话 {Id}"} - {Title}";
    }

    [Reactive] public partial object? Source { get; set; }
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
    [Reactive] public partial EpisodeCollectionType? Status { get; set; }
    [Reactive] public partial DateTimeOffset? StatusUpdateTime { get; set; }

    [Reactive] public partial INeighboring? Prev { get; set; }
    [Reactive] public partial INeighboring? Next { get; set; }
    [Reactive] public partial EpisodeListViewModel? Parent { get; set; }

    public ReactiveCommand<Unit, INeighboring?>? ShowPrevCommand { get; private set; }
    public ReactiveCommand<Unit, INeighboring?>? ShowNextCommand { get; private set; }
    public ICommand? UncollectCommand { get; private set; }
    public ICommand? WishCommand { get; private set; }
    public ICommand? DoneCommand { get; private set; }
    public ICommand? DropCommand { get; private set; }
    public ICommand? DoneUntilCommand { get; private set; }

    public bool ShouldDisplayDurationString => Duration == null && !string.IsNullOrWhiteSpace(DurationString);

    // 尽管对非正片或SP话更新收藏状态会导致完成度显示异常，还是应该允许这么做。
    public async Task UpdateStatus(EpisodeCollectionType type)
    {
        if (Id == null) return;
        var result = await ApiC.UpdateEpisodeCollection((int)Id, type);
        switch (result)
        {
            case 204:
                Status = type;
                StatusUpdateTime = DateTimeOffset.Now;
                break;
            case 400:
                MessageWindow.ShowMessage("未收藏本话所属项目。");
                break;
            case 401:
                MessageWindow.ShowMessage("未登录。");
                break;
            case 404:
                MessageWindow.ShowMessage("条目或者章节不存在。");
                break;
            default: break;
        }
    }
    public async Task UpdateStatusUntilThis(EpisodeCollectionType type)
    {
        if (Ep == null || Parent == null) return;

        var affectedEps = Parent.EpisodeViewModels.Where(x => x.Ep != null && x.Ep <= Ep && x.Id != null);

        var result = await ApiC.UpdateEpisodeCollection(Parent.SubjectId,
            affectedEps.Select(x => (int)x.Id!), type);
        switch (result)
        {
            case 204:
                foreach (var ep in affectedEps)
                {
                    ep.Status = type;
                    ep.StatusUpdateTime = DateTimeOffset.Now;
                }
                break;
            case 400:
                MessageWindow.ShowMessage("未收藏本话所属项目。");
                break;
            case 401:
                MessageWindow.ShowMessage("未登录。");
                break;
            case 404:
                MessageWindow.ShowMessage("条目或者章节不存在。");
                break;
            default: break;
        }
    }
}
