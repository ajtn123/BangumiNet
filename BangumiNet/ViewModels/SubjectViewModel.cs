using BangumiNet.Api.Interfaces;
using BangumiNet.Api.Legacy.Models;
using BangumiNet.Api.V0.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Utils;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;

namespace BangumiNet.ViewModels;

/// <summary>
/// 通用 Subject 视图模型
/// </summary>
public partial class SubjectViewModel : ViewModelBase
{
    public SubjectViewModel(SlimSubject subject)
    {
        SourceType = subject.GetType();
        CollectionTotal = subject.CollectionTotal;
        Rank = subject.Rank;
        Eps = subject.Eps;
        Volumes = subject.Volumes;
        ShortSummary = subject.ShortSummary;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Date = Common.ParseDate(subject.Date);
        Id = subject.Id;
        Score = subject.Score;
        Type = (SubjectType?)subject.Type;
        Tags = subject.Tags.ToObservableCollection();
        Images = subject.Images;
    }
    public SubjectViewModel(Legacy_SubjectSmall subject)
    {
        SourceType = subject.GetType();
        Eps = subject.Eps;
        Rank = subject.Rank;
        Date = Common.ParseDate(subject.AirDate);
        Id = subject.Id;
        Url = subject.Url;
        Type = (SubjectType?)subject.Type;
        Images = subject.Images;
        Summary = subject.Summary;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Collection = subject.Collection;
    }

    [Reactive] public partial Type? SourceType { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? Rank { get; set; }
    [Reactive] public partial int? Eps { get; set; }
    [Reactive] public partial int? Volumes { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? ShortSummary { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? NameCn { get; set; }
    [Reactive] public partial DateOnly? Date { get; set; }
    [Reactive] public partial DayOfWeek? Weekday { get; set; }
    [Reactive] public partial double? Score { get; set; }
    [Reactive] public partial SubjectType? Type { get; set; }
    [Reactive] public partial ObservableCollection<Collections>? Tags { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial ICollection? Collection { get; set; }

    [Reactive] public partial string? Url { get; set; }
}
