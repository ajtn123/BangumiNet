using BangumiNet.Api.Extensions;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Common;
using BangumiNet.Common.Attributes;
using BangumiNet.Common.Extras;
using BangumiNet.Converters;
using BangumiNet.Models;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Disposables.Fluent;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

/// <summary>
/// 通用 Subject 视图模型
/// </summary>
public partial class SubjectViewModel : ItemViewModelBase
{
    public SubjectViewModel(Api.P1.Models.SlimSubject subject)
    {
        Source = subject;
        Rank = subject.Rating?.Rank;
        Name = subject.Name;
        NameCn = subject.NameCN;
        Id = subject.Id;
        Score = subject.Rating?.Score;
        Type = (SubjectType?)subject.Type;
        Info = subject.Info;
        Images = subject.Images;
        RatingCount = subject.Rating?.Count?.Select(x => x ?? 0).ToList();
        RatingTotal = subject.Rating?.Total;
        IsNsfw = subject.Nsfw ?? false;
        IsLocked = subject.Locked ?? false;
        if (subject.Interest != null)
            SubjectCollectionViewModel = new(subject.Interest) { Parent = this };
    }
    public SubjectViewModel(SlimSubject subject)
    {
        Source = subject;
        CollectionTotal = subject.CollectionTotal;
        Rank = subject.Rank;
        Eps = subject.Eps;
        VolumeCount = subject.Volumes;
        Summary = subject.ShortSummary;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Date = CommonUtils.ParseBangumiDate(subject.Date);
        Id = subject.Id;
        Score = subject.Score;
        Type = (SubjectType?)subject.Type;
        Tags = subject.Tags?.ToObservableCollection<ITag>();
        Images = subject.Images;
    }
    [Obsolete]
    public SubjectViewModel(Api.Legacy.Models.Legacy_SubjectSmall subject)
    {
        Source = subject;
        Eps = subject.Eps;
        Rank = subject.Rank;
        Date = CommonUtils.ParseBangumiDate(subject.AirDate);
        Id = subject.Id;
        Url = subject.Url;
        Type = (SubjectType?)subject.Type;
        Images = subject.Images;
        Summary = subject.Summary;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Collection = subject.Collection;
        Weekday = CommonUtils.ParseDayOfWeek(subject.AirWeekday);
        Score = subject.Rating?.Score;
        RatingCount = (subject.Rating?.Count as IRatingCount)?.ToList();
        RatingTotal = subject.Rating?.Total;
    }
    public SubjectViewModel(Subject subject)
    {
        Source = subject;
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Summary = subject.Summary;
        IsSeries = subject.Series ?? false;
        IsNsfw = subject.Nsfw ?? false;
        IsLocked = subject.Locked ?? false;
        Date = CommonUtils.ParseBangumiDate(subject.Date);
        Platform = subject.Platform;
        Images = subject.Images;
        Infobox = subject.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();
        VolumeCount = subject.Volumes;
        Eps = subject.Eps;
        EpisodeCount = subject.TotalEpisodes;
        Rank = subject.Rating?.Rank;
        RatingTotal = subject.Rating?.Total;
        RatingCount = (subject.Rating?.Count as IRatingCount)?.ToList();
        Score = subject.Rating?.Score;
        Collection = subject.Collection;
        CollectionTotal = Collection?.GetTotal();
        Tags = subject.Tags?.Select(t => new Tag(t)).ToObservableCollection<ITag>();
        MetaTags = subject.MetaTags?.ToObservableCollection();
    }
    public SubjectViewModel(V0_subject_relation subject)
    {
        Source = subject;
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Images = subject.Images;
        Relation = subject.Relation;
    }
    public SubjectViewModel(V0_RelatedSubject subject)
    {
        Source = subject;
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Images = new ImageSet() { Large = subject.Image };
        Relation = subject.Staff;
        if (!string.IsNullOrWhiteSpace(subject.Eps))
            Relation += $" < {subject.Eps}";
    }
    public SubjectViewModel(SubjectBasic subject)
    {
        Name = subject.Name;
        NameCn = subject.NameCn;
        Id = subject.Id;
        Type = subject.Type;
        Relation = subject.Type?.GetNameCn();
    }
    public SubjectViewModel(Api.P1.Models.Subject subject)
    {
        Source = subject;
        Id = subject.Id;
        Type = (SubjectType?)subject.Type;
        Name = subject.Name;
        NameCn = subject.NameCN;
        Summary = subject.Summary;
        IsSeries = subject.Series ?? false;
        IsNsfw = subject.Nsfw ?? false;
        IsLocked = subject.Locked ?? false;
        Date = CommonUtils.ParseBangumiDate(subject.Airtime?.Date);
        Platform = subject.Platform?.TypeCN;
        Images = subject.Images;
        Infobox = subject.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();
        VolumeCount = subject.Volumes;
        EpisodeCount = subject.Eps;
        Rank = subject.Rating?.Rank;
        RatingTotal = subject.Rating?.Total;
        RatingCount = subject.Rating?.Count?.Select(c => c ?? 0).ToList();
        Score = subject.Rating?.Score;
        Collection = subject.Collection;
        CollectionTotal = Collection?.GetTotal();
        Tags = subject.Tags?.ToObservableCollection<ITag>();
        MetaTags = subject.MetaTags?.ToObservableCollection();
    }
    public static SubjectViewModel Init(Api.P1.Models.SubjectRelation relation)
        => new(relation.Subject!)
        {
            Relation = relation.Relation?.ToLocalString(),
            Order = relation.Order,
        };
    public static SubjectViewModel Init(Api.P1.Models.PersonWork work)
        => new(work.Subject!)
        {
            Relation = string.Join(' ', work.Positions?.Select(x => x.Type?.ToLocalString()) ?? []),
        };
    public static SubjectViewModel Init(Api.P1.Models.CharacterSubject characterSubject)
        => new(characterSubject.Subject!)
        {
            Relation = ((CharacterRole?)characterSubject.Type)?.GetNameCn() + "·" + string.Join('/', characterSubject.Actors?.Select(NameCnCvt.Convert) ?? []),
            RelationItems = new() { SubjectViewModels = characterSubject.Actors?.Select<Api.P1.Models.SlimPerson, ViewModelBase>(x => new PersonViewModel(x)).ToObservableCollection() }
        };
    public static SubjectViewModel Init(Api.P1.Models.SubjectRec rec)
        => new(rec.Subject!)
        {
            Hype = rec.Count,
            Similarity = rec.Sim,
        };

    protected override void Activate(CompositeDisposable disposables)
    {
        if (RatingCount != null) SubjectRatingViewModel ??= new(RatingCount);
        EpisodeListViewModel ??= new(RelatedItemType.Episode, ItemType, Id);
        PersonBadgeListViewModel ??= new(RelatedItemType.Person, ItemType, Id);
        CharacterBadgeListViewModel ??= new(RelatedItemType.Character, ItemType, Id);
        SubjectBadgeListViewModel ??= new(RelatedItemType.Subject, ItemType, Id);
        BlogCardListViewModel ??= new(RelatedItemType.Review, ItemType, Id);
        TopicCardListViewModel ??= new(RelatedItemType.Topic, ItemType, Id);
        Recommendations ??= new(RelatedItemType.Recommendation, ItemType, Id);
        IndexCardListViewModel ??= new(RelatedItemType.Index, ItemType, Id);
        CommentListViewModel ??= new(ItemType, Id);
        RevisionListViewModel ??= new(this);

        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvSubjectUrlBase + Id)).DisposeWith(disposables);
        CollectCommand = ReactiveCommand.Create(() => new SubjectCollectionEditWindow() { DataContext = new SubjectCollectionViewModel(this) }.Show(),
            this.WhenAnyValue(x => x.SubjectCollectionViewModel).Select(c => c == null)).DisposeWith(disposables);
        ShowCoversCommand = ReactiveCommand.Create(() =>
        {
            var vm = new RelatedItemListViewModel(RelatedItemType.Cover, ItemType, Id);
            _ = vm.LoadAsync();
            SecondaryWindow.Show(vm);
        }).DisposeWith(disposables);

        this.WhenAnyValue(x => x.Source).Subscribe(e => this.RaisePropertyChanged(nameof(IsFull))).DisposeWith(disposables);
        this.WhenAnyValue(x => x.Tags, x => x.MetaTags).Subscribe(e =>
        {
            this.RaisePropertyChanged(nameof(TagListViewModel));
            Tags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
            MetaTags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
        }).DisposeWith(disposables);

        if (Rank == 0) Rank = null;
        if (Eps == 0) Eps = null;
        if (EpisodeCount == 0) EpisodeCount = null;
        if (VolumeCount == 0) VolumeCount = null;
        if (string.IsNullOrWhiteSpace(Summary)) Summary = null;
        EpisodeCount ??= Eps;
    }

    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? Rank { get; set; }
    [Reactive] public partial int? Eps { get; set; }
    [Reactive] public partial int? EpisodeCount { get; set; }
    [Reactive] public partial int? VolumeCount { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? Info { get; set; }
    [Reactive] public partial DateOnly? Date { get; set; }
    [Reactive] public partial DayOfWeek? Weekday { get; set; }
    [Reactive] public partial double? Score { get; set; }
    [Reactive] public partial int? RatingTotal { get; set; }
    [Reactive] public partial List<int>? RatingCount { get; set; }
    [Reactive] public partial SubjectType? Type { get; set; }
    [Reactive] public partial ObservableCollection<ITag>? Tags { get; set; }
    [Reactive] public partial ObservableCollection<string>? MetaTags { get; set; }
    [Reactive] public partial IImagesGrid? Images { get; set; }
    [Reactive] public partial ICollection? Collection { get; set; }
    [Reactive] public partial int? Hype { get; set; }
    [Reactive] public partial double? Similarity { get; set; }
    [Reactive] public partial bool IsSeries { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial bool IsLocked { get; set; }
    [Reactive] public partial string? Platform { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial string? Url { get; set; }
    [Reactive] public partial RelatedItemListViewModel? EpisodeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? PersonBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? CharacterBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? BlogCardListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? TopicCardListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? Recommendations { get; set; }
    [Reactive] public partial RelatedItemListViewModel? IndexCardListViewModel { get; set; }
    [Reactive] public partial SubjectCollectionViewModel? SubjectCollectionViewModel { get; set; }
    [Reactive] public partial SubjectRatingViewModel? SubjectRatingViewModel { get; set; }
    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial CommentListViewModel? CommentListViewModel { get; set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit>? CollectCommand { get; private set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit>? ShowCoversCommand { get; private set; }

    public TagListViewModel? TagListViewModel => new(Tags, MetaTags, Type);
    public bool IsFull => Source is Api.P1.Models.Subject;
    public override ItemType ItemType { get; init; } = ItemType.Subject;
}
