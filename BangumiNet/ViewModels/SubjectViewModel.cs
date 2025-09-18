using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.Legacy.Models;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using BangumiNet.Models;
using BangumiNet.Shared;
using BangumiNet.Utils;
using BangumiNet.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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
        Tags = subject.Tags.ToObservableCollection<ITag>();
        Images = subject.Images;

        Init();
    }
    public SubjectViewModel(Legacy_SubjectSmall subject)
    {
        IsLegacy = true;
        Source = subject;
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
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Summary = subject.Summary;
        IsSeries = subject.Series;
        IsNsfw = subject.Nsfw;
        IsLocked = subject.Locked;
        Date = Common.ParseDate(subject.Date);
        Platform = subject.Platform;
        Images = subject.Images;
        Infobox = subject.Infobox?.Select(p => new InfoboxItem(p)).ToObservableCollection();
        Volumes = subject.Volumes;
        Eps = subject.Eps;
        TotalEps = subject.TotalEpisodes;
        Rank = subject.Rating?.Rank;
        RatingTotal = subject.Rating?.Total;
        RatingCount = subject.Rating?.Count;
        Score = subject.Rating?.Score;
        Collection = subject.Collection;
        CollectionTotal = Collection?.GetTotal();
        Tags = subject.Tags?.Select(t => new Tag(t)).ToObservableCollection<ITag>();
        MetaTags = subject.MetaTags.ToObservableCollection();

        Init();
    }

    public void Init()
    {
        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new SubjectView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvSubjectUrlBase + Id));

        this.WhenAnyValue(x => x.Name, x => x.NameCn).Subscribe(e => this.RaisePropertyChanged(nameof(ParentWindowTitle)));
        this.WhenAnyValue(x => x.Tags, x => x.MetaTags).Subscribe(e =>
        {
            this.RaisePropertyChanged(nameof(TagListViewModel));
            Tags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
            MetaTags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
        });

        if (Rank == 0) Rank = null;
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public bool IsLegacy { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? Rank { get; set; }
    [Reactive] public partial int? Eps { get; set; }
    [Reactive] public partial int? TotalEps { get; set; }
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
    [Reactive] public partial ObservableCollection<ITag>? Tags { get; set; }
    [Reactive] public partial ObservableCollection<string>? MetaTags { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial ICollection? Collection { get; set; }
    [Reactive] public partial bool? IsSeries { get; set; }
    [Reactive] public partial bool? IsNsfw { get; set; }
    [Reactive] public partial bool? IsLocked { get; set; }
    [Reactive] public partial string? Platform { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItem>? Infobox { get; set; }
    [Reactive] public partial string? Url { get; set; }

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchGoogleCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageCommon => IsLegacy ? new(() => null) : ApiC.GetImageAsync(Images?.Common, !IsLegacy);
    public Task<Bitmap?> ImageSmall => IsLegacy ? new(() => null) : ApiC.GetImageAsync(Images?.Small, !IsLegacy);
    public Task<Bitmap?> ImageMedium => IsLegacy ? new(() => null) : ApiC.GetImageAsync(Images?.Medium, !IsLegacy);
    public Task<Bitmap?> ImageLarge => IsLegacy ? new(() => null) : ApiC.GetImageAsync(Images?.Large, !IsLegacy);

    public string ParentWindowTitle => $"{NameCnConverter.Convert(this)} - {Constants.ApplicationName}";
    public TagListViewModel? TagListViewModel => new(Tags, MetaTags);
}
