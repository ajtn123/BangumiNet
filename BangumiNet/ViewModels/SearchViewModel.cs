using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.V0.V0.Search.Subjects;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class SearchViewModel : ViewModelBase
{
    public SearchViewModel()
    {
        SearchType = SearchType.Subject;
        Type = SubjectTypeOptionViewModel.GetList();
        PageNavigatorViewModel = new PageNavigatorViewModel();
        Tag = []; MetaTag = [];

        SearchCommand = ReactiveCommand.CreateFromTask(SearchAsync, this.WhenAnyValue(x => x.IsFilterValidR));
        AddTagCommand = ReactiveCommand.Create(() => { if (!string.IsNullOrWhiteSpace(TagInput) && !Tag.Contains(TagInput)) { Tag.Add(TagInput.Trim()); TagInput = string.Empty; } },
            this.WhenAnyValue(x => x.TagInput).Select(y => !string.IsNullOrWhiteSpace(y) && !Tag.Contains(y)));
        AddMetaTagCommand = ReactiveCommand.Create(() => { if (!string.IsNullOrWhiteSpace(MetaTagInput) && !MetaTag.Contains(MetaTagInput)) { MetaTag.Add(MetaTagInput.Trim()); MetaTagInput = string.Empty; } },
            this.WhenAnyValue(x => x.MetaTagInput).Select(y => !string.IsNullOrWhiteSpace(y) && !MetaTag.Contains(y)));
        DelTagCommand = ReactiveCommand.Create<string>(s => { if (s != null && Tag.Contains(s)) Tag.Remove(s); });
        DelMetaTagCommand = ReactiveCommand.Create<string>(s => { if (s != null && MetaTag.Contains(s)) MetaTag.Remove(s); });

        this.WhenAnyValue(x => x.IsAirDateEnabled, x => x.AirDateStart, x => x.AirDateEnd)
            .Subscribe(x => { this.RaisePropertyChanged(nameof(IsAirDateValid)); IsFilterValidR = IsFilterValid; });
        this.WhenAnyValue(x => x.IsRankEnabled, x => x.RankLowerLimit, x => x.RankUpperLimit)
            .Subscribe(x => { this.RaisePropertyChanged(nameof(IsRankValid)); IsFilterValidR = IsFilterValid; });
        this.WhenAnyValue(x => x.IsAirDateEnabled, x => x.RatingLowerLimit, x => x.RatingUpperLimit)
            .Subscribe(x => { this.RaisePropertyChanged(nameof(IsRatingValid)); IsFilterValidR = IsFilterValid; });
        this.WhenAnyValue(x => x.ResultOffset, x => x.TotalResults)
            .Subscribe(x => ResultOffsetMessage = x.Item1 == null || x.Item2 == null ? null : $"正在显示第 {x.Item1 + 1}-{Math.Min((int)x.Item1 + Limit, (int)x.Item2)} 条结果");

        PageNavigatorViewModel.PrevPage.Subscribe(i => _ = SearchPageAsync(i));
        PageNavigatorViewModel.NextPage.Subscribe(i => _ = SearchPageAsync(i));
        PageNavigatorViewModel.JumpPage.Subscribe(i => _ = SearchPageAsync(i));
    }

    public async Task SearchAsync()
    {
        SubjectsPostRequestBody = new()
        {
            Keyword = Keyword,
            Sort = Sort,
            Filter = new SubjectsPostRequestBody_filter()
            {
                AirDate = GetAirDateFilter(),
                MetaTags = GetMetaTagFilter(),
                Nsfw = Nsfw,
                Rank = GetRankFilter(),
                Rating = GetRatingFilter(),
                Tag = GetTagFilter(),
                Type = GetTypeFilter(),
            }
        };
        var response = await ApiC.V0.Search.Subjects.PostAsync(SubjectsPostRequestBody, config =>
        {
            config.QueryParameters.Offset = 0;
            config.QueryParameters.Limit = Limit;
        });
        if (response == null) { SubjectsPostRequestBody = null; return; }
        SubjectListViewModel = new();
        SubjectListViewModel.AddSubjects(response);
        PageNavigatorViewModel.PageIndex = 1;
        TotalResults = response.Total;
        ResultOffset = response.Offset;
        if (response.Total != null)
            PageNavigatorViewModel.Total = (int)Math.Ceiling((double)response.Total / Limit);
        else PageNavigatorViewModel.Total = null;
    }

    public async Task SearchPageAsync(int? i)
    {
        if (SubjectsPostRequestBody == null) return;
        if (i is not int pageIndex || !PageNavigatorViewModel.IsInRange(i)) return;
        var response = await ApiC.V0.Search.Subjects.PostAsync(SubjectsPostRequestBody, config =>
        {
            config.QueryParameters.Offset = (pageIndex - 1) * Limit;
            config.QueryParameters.Limit = Limit;
        });
        if (response == null) { SubjectsPostRequestBody = null; return; }
        SubjectListViewModel = new();
        SubjectListViewModel.AddSubjects(response);
        PageNavigatorViewModel.PageIndex = pageIndex;
        TotalResults = response.Total;
        ResultOffset = response.Offset;
        if (response.Total != null)
            PageNavigatorViewModel.Total = (int)Math.Ceiling((double)response.Total / Limit);
        else PageNavigatorViewModel.Total = null;
    }

    public const int Limit = 10;

    [Reactive] public partial string? ErrorMessage { get; set; }
    [Reactive] public partial string? Keyword { get; set; }
    [Reactive] public partial SearchType SearchType { get; set; }
    [Reactive] public partial SubjectsPostRequestBody_sort? Sort { get; set; }
    [Reactive] public partial List<SubjectTypeOptionViewModel> Type { get; set; }
    [Reactive] public partial ObservableCollection<string> Tag { get; set; }
    [Reactive] public partial ObservableCollection<string> MetaTag { get; set; }
    [Reactive] public partial string? TagInput { get; set; }
    [Reactive] public partial string? MetaTagInput { get; set; }
    [Reactive] public partial bool IsAirDateEnabled { get; set; }
    [Reactive] public partial DateTimeOffset? AirDateStart { get; set; }
    [Reactive] public partial DateTimeOffset? AirDateEnd { get; set; }
    [Reactive] public partial bool IsRankEnabled { get; set; }
    [Reactive] public partial int? RankLowerLimit { get; set; }
    [Reactive] public partial int? RankUpperLimit { get; set; }
    [Reactive] public partial bool IsRatingEnabled { get; set; }
    [Reactive] public partial double? RatingLowerLimit { get; set; }
    [Reactive] public partial double? RatingUpperLimit { get; set; }
    [Reactive] public partial bool? Nsfw { get; set; }
    [Reactive] public partial SubjectListViewModel? SubjectListViewModel { get; set; }
    [Reactive] public partial SubjectsPostRequestBody? SubjectsPostRequestBody { get; set; }
    [Reactive] public partial int? TotalResults { get; set; }
    [Reactive] public partial int? ResultOffset { get; set; }
    [Reactive] public partial string? ResultOffsetMessage { get; set; }
    [Reactive] public partial PageNavigatorViewModel PageNavigatorViewModel { get; set; }

    public ICommand SearchCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddTagCommand { get; set; }
    public ReactiveCommand<string, Unit> DelTagCommand { get; set; }
    public ReactiveCommand<Unit, Unit> AddMetaTagCommand { get; set; }
    public ReactiveCommand<string, Unit> DelMetaTagCommand { get; set; }

    public List<int?>? GetTypeFilter()
    {
        var list = Type?.Where(x => x.IsSelected).Select(x => (int?)x.SubjectType).ToList();
        if (list?.Count > 0) return list;
        else return null;
    }
    public List<string>? GetAirDateFilter()
    {
        if (!IsAirDateEnabled) return null;
        var list = new List<string>();
        if (AirDateStart.HasValue)
            list.Add($"{SearchFilterRelation.GreaterThanOrEqualTo.ToSymbol()}{AirDateStart.Value.ToBangumiString()}");
        if (AirDateEnd.HasValue)
            list.Add($"{SearchFilterRelation.LessThanOrEqualTo.ToSymbol()}{AirDateEnd.Value.ToBangumiString()}");
        if (list.Count > 0) return list;
        else return null;
    }
    public List<string>? GetRatingFilter()
    {
        if (!IsRatingEnabled) return null;
        var list = new List<string>();
        if (RatingLowerLimit.HasValue)
            list.Add($"{SearchFilterRelation.GreaterThanOrEqualTo.ToSymbol()}{RatingLowerLimit.Value}");
        if (RatingUpperLimit.HasValue)
            list.Add($"{SearchFilterRelation.LessThanOrEqualTo.ToSymbol()}{RatingUpperLimit.Value}");
        if (list.Count > 0) return list;
        else return null;
    }
    public List<string>? GetRankFilter()
    {
        if (!IsRankEnabled) return null;
        var list = new List<string>();
        if (RankLowerLimit.HasValue)
            list.Add($"{SearchFilterRelation.GreaterThanOrEqualTo.ToSymbol()}{RankLowerLimit.Value}");
        if (RankUpperLimit.HasValue)
            list.Add($"{SearchFilterRelation.LessThanOrEqualTo.ToSymbol()}{RankUpperLimit.Value}");
        if (list.Count > 0) return list;
        else return null;
    }
    public List<string>? GetTagFilter()
    {
        if (Tag.Count > 0) return [.. Tag];
        else return null;
    }
    public List<string>? GetMetaTagFilter()
    {
        if (MetaTag.Count > 0) return [.. MetaTag];
        else return null;
    }

    [Reactive] public partial bool IsFilterValidR { get; set; }
    public bool IsFilterValid => IsAirDateValid && IsRankValid && IsRatingValid;

    public bool IsAirDateValid => !IsAirDateEnabled || AirDateEnd >= AirDateStart;
    public bool IsRankValid => !IsRankEnabled || RankUpperLimit >= RankLowerLimit;
    public bool IsRatingValid => !IsRatingEnabled || RatingUpperLimit >= RatingLowerLimit;
}

public partial class SubjectTypeOptionViewModel : ViewModelBase
{
    public static List<SubjectTypeOptionViewModel> GetList()
        => [.. Enum.GetValues<SubjectType>().Select(x => new SubjectTypeOptionViewModel(x, x.ToStringSC()))];

    private SubjectTypeOptionViewModel(SubjectType v, string? name = null)
    {
        SubjectType = v;
        DisplayName = name ?? v.ToString();
    }

    [Reactive] public partial SubjectType SubjectType { get; set; }
    [Reactive] public partial string DisplayName { get; set; }
    [Reactive] public partial bool IsSelected { get; set; }
}
public enum SearchFilterRelation
{
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo,
    EqualTo
}
public enum SearchType
{
    Subject,
    Character,
    Person
}
