using Avalonia.Media.Imaging;
using BangumiNet.Api.Extensions;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using BangumiNet.Models;
using System.Reactive.Linq;
using System.Windows.Input;

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

        Init();
    }
    public SubjectViewModel(SlimSubject subject)
    {
        Source = subject;
        CollectionTotal = subject.CollectionTotal;
        Rank = subject.Rank;
        Eps = subject.Eps;
        Volumes = subject.Volumes;
        Summary = subject.ShortSummary;
        Name = subject.Name;
        NameCn = subject.NameCn;
        Date = Common.ParseBangumiDate(subject.Date);
        Id = subject.Id;
        Score = subject.Score;
        Type = (SubjectType?)subject.Type;
        Tags = subject.Tags?.ToObservableCollection<ITag>();
        Images = subject.Images;

        Init();
    }
    [Obsolete]
    public SubjectViewModel(Api.Legacy.Models.Legacy_SubjectSmall subject)
    {
        Source = subject;
        Eps = subject.Eps;
        Rank = subject.Rank;
        Date = Common.ParseBangumiDate(subject.AirDate);
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
        RatingCount = (subject.Rating?.Count as IRatingCount)?.ToList();
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
        IsSeries = subject.Series ?? false;
        IsNsfw = subject.Nsfw ?? false;
        IsLocked = subject.Locked ?? false;
        Date = Common.ParseBangumiDate(subject.Date);
        Platform = subject.Platform;
        Images = subject.Images;
        Infobox = subject.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();
        Volumes = subject.Volumes;
        Eps = subject.Eps;
        TotalEps = subject.TotalEpisodes;
        Rank = subject.Rating?.Rank;
        RatingTotal = subject.Rating?.Total;
        RatingCount = (subject.Rating?.Count as IRatingCount)?.ToList();
        Score = subject.Rating?.Score;
        Collection = subject.Collection;
        CollectionTotal = Collection?.GetTotal();
        Tags = subject.Tags?.Select(t => new Tag(t)).ToObservableCollection<ITag>();
        MetaTags = subject.MetaTags?.ToObservableCollection();

        Init();
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

        Init();
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

        Init();
    }
    public SubjectViewModel(SubjectBasic subject)
    {
        Name = subject.Name;
        NameCn = subject.NameCn;
        Id = subject.Id;
        Type = subject.Type;
        Relation = subject.Type?.ToStringSC();

        Init();
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
        Date = Common.ParseBangumiDate(subject.Airtime?.Date);
        Platform = subject.Platform?.TypeCN;
        Images = subject.Images;
        Infobox = subject.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();
        Volumes = subject.Volumes;
        // Eps = subject.Eps;
        TotalEps = subject.Eps;
        Rank = subject.Rating?.Rank;
        RatingTotal = subject.Rating?.Total;
        RatingCount = subject.Rating?.Count?.Select(c => c ?? 0).ToList();
        Score = subject.Rating?.Score;
        Collection = subject.Collection;
        CollectionTotal = Collection?.GetTotal();
        Tags = subject.Tags?.ToObservableCollection<ITag>();
        MetaTags = subject.MetaTags?.ToObservableCollection();

        Init();
    }
    public static SubjectViewModel Init(Api.P1.Models.SubjectRelation relation)
        => new(relation.Subject!)
        {
            Relation = relation.Relation?.ToLocalString(),
            Order = relation.Order,
        };
    private void Init()
    {
        ItemType = ItemType.Subject;

        if (IsFull && RatingCount != null) SubjectRatingViewModel = new(RatingCount);
        if (Id != null)
        {
            EpisodeListViewModel = new((int)Id);
            PersonBadgeListViewModel = new(RelatedItemType.Person, ItemType, Id);
            CharacterBadgeListViewModel = new(RelatedItemType.Character, ItemType, Id);
            SubjectBadgeListViewModel = new(RelatedItemType.Subject, ItemType, Id);
            CommentListViewModel = new(ItemType, Id);
            RevisionListViewModel = new(this);
        }

        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(Url ?? UrlProvider.BangumiTvSubjectUrlBase + Id));
        CollectCommand = ReactiveCommand.Create(() => new SubjectCollectionEditWindow() { DataContext = new SubjectCollectionViewModel(this) }.Show(),
            this.WhenAnyValue(x => x.SubjectCollectionViewModel).Select(c => c == null));

        this.WhenAnyValue(x => x.Source).Subscribe(e => this.RaisePropertyChanged(nameof(IsFull)));
        this.WhenAnyValue(x => x.Tags, x => x.MetaTags).Subscribe(e =>
        {
            this.RaisePropertyChanged(nameof(TagListViewModel));
            Tags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
            MetaTags?.CollectionChanged += (s, e) => this.RaisePropertyChanged(nameof(TagListViewModel));
        });

        if (Rank == 0) Rank = null;
        if (Eps == 0) Eps = null;
        if (TotalEps == 0) TotalEps = null;
        if (Volumes == 0) Volumes = null;
        if (string.IsNullOrWhiteSpace(Summary)) Summary = null;

        Title = $"{NameCnCvt.Convert(this) ?? $"项目 {Id}"} - {Title}";
    }

    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? Rank { get; set; }
    [Reactive] public partial int? Eps { get; set; }
    [Reactive] public partial int? TotalEps { get; set; }
    [Reactive] public partial int? Volumes { get; set; }
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
    [Reactive] public partial bool IsSeries { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial bool IsLocked { get; set; }
    [Reactive] public partial string? Platform { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial string? Url { get; set; }
    [Reactive] public partial EpisodeListViewModel? EpisodeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? PersonBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? CharacterBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial SubjectCollectionViewModel? SubjectCollectionViewModel { get; set; }
    [Reactive] public partial SubjectRatingViewModel? SubjectRatingViewModel { get; set; }
    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial CommentListViewModel? CommentListViewModel { get; set; }

    public ICommand? CollectCommand { get; private set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public TagListViewModel? TagListViewModel => new(Tags, MetaTags, Type);
    public bool IsFull => Source is Api.P1.Models.Subject;
}
