using Avalonia.Media.Imaging;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.Legacy.Models;
using BangumiNet.Api.V0.ExtraEnums;
using BangumiNet.Api.V0.Models;
using BangumiNet.Shared;
using BangumiNet.Utils;
using BangumiNet.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

/// <summary>
/// 通用 Subject 视图模型
/// </summary>
public partial class SubjectViewModel : ViewModelBase
{
    public SubjectViewModel(SlimSubject subject)
    {
        Source = subject;
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

        Init();
    }
    public SubjectViewModel(Legacy_SubjectSmall subject)
    {
        Source = subject;
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
        Weekday = Common.ParseDayOfWeek(subject.AirWeekday);
        Score = subject.Rating?.Score;
        RatingCount = subject.Rating?.Count;
        RatingTotal = subject.Rating?.Total;

        Init();
    }
    public SubjectViewModel(Subject subject)
    {
        Source = subject;
        SourceType = subject.GetType();
        Eps = subject.Eps;
        Volumes = subject.Volumes;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Date = Common.ParseDate(subject.Date);
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Images = subject.Images;

        Init();
    }

    public void Init()
    {
        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new SubjectView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvSubjectUrlBase + Id));

        if (Rank == 0) Rank = null;
    }

    [Reactive] public partial object? Source { get; set; }
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
    [Reactive] public partial int? RatingTotal { get; set; }
    [Reactive] public partial IRatingCount? RatingCount { get; set; }
    [Reactive] public partial SubjectType? Type { get; set; }
    [Reactive] public partial ObservableCollection<Collections>? Tags { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial ICollection? Collection { get; set; }

    [Reactive] public partial string? Url { get; set; }

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchGoogleCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }

    public Task<Bitmap?> ImageCommon => ApiC.GetImageAsync(Images?.Common);
    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);
}
