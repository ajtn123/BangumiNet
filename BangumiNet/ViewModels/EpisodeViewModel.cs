using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Shared.Interfaces;
using BangumiNet.Utils;
using ReactiveUI.SourceGenerators;
using System;

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
        AirDate = Common.ParseDate(episode.Airdate);
        Comment = episode.Comment;
        Duration = episode.Duration;
        Desc = episode.Desc;
        Disc = episode.Disc;
        DurationSecond = episode.DurationSeconds != 0 ? episode.DurationSeconds : null;
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial EpisodeType? Type { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial double? Sort { get; set; }
    [Reactive] public partial double? Ep { get; set; }
    [Reactive] public partial DateOnly? AirDate { get; set; }
    [Reactive] public partial int? Comment { get; set; }
    [Reactive] public partial string? Duration { get; set; }
    [Reactive] public partial string? Desc { get; set; }
    [Reactive] public partial int? Disc { get; set; }
    [Reactive] public partial int? DurationSecond { get; set; }

    [Reactive] public partial INeighboring? Prev { get; set; }
    [Reactive] public partial INeighboring? Next { get; set; }
}
