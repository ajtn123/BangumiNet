using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.V0.Models;
using BangumiNet.Api.V0.V0.Search.Characters;
using BangumiNet.Api.V0.V0.Search.Persons;
using BangumiNet.Api.V0.V0.Search.Subjects;
using BangumiNet.Common;
using BangumiNet.Common.Attributes;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class SearchViewModel : SubjectListPagedViewModel
{
    public SearchViewModel()
    {
        Title = $"搜索 - {Title}";
        SearchType = ItemType.Subject;
        Type = SubjectTypeOptionViewModel.GetList();
        Career = PersonCareerOptionViewModel.GetList();
        Tag = []; MetaTag = [];
        RatingLowerLimit = 1;
        RatingUpperLimit = 10;

        SearchCommand = ReactiveCommand.CreateFromTask(ct => LoadPageAsync(-1, ct), this.WhenAnyValue(x => x.IsFilterValidR));
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
        this.WhenAnyValue(x => x.IsRatingEnabled, x => x.RatingLowerLimit, x => x.RatingUpperLimit)
            .Subscribe(x => { this.RaisePropertyChanged(nameof(IsRatingValid)); IsFilterValidR = IsFilterValid; });
        this.WhenAnyValue(x => x.IsRatingCountEnabled, x => x.RatingCountLowerLimit, x => x.RatingCountUpperLimit)
            .Subscribe(x => { this.RaisePropertyChanged(nameof(IsRatingCountValid)); IsFilterValidR = IsFilterValid; });
        this.WhenAnyValue(x => x.SearchType).Subscribe(a =>
        {
            this.RaisePropertyChanged(nameof(IsSearchingSubject));
            this.RaisePropertyChanged(nameof(IsSearchingPerson));
            this.RaisePropertyChanged(nameof(IsSearchingCharacter));
        });
    }

    protected override async Task LoadPageAsync(int? page, CancellationToken cancellationToken = default)
    {
        if (page is not int p) return;
        var offset = (p - 1) * Limit;

        try
        {
            if (p != -1 && Request != null)
                await (Request switch
                {
                    SubjectsPostRequestBody sr => SearchSubjectPageAsync(offset, sr, cancellationToken),
                    CharactersPostRequestBody cr => SearchCharacterPageAsync(offset, cr, cancellationToken),
                    PersonsPostRequestBody pr => SearchPersonPageAsync(offset, pr, cancellationToken),
                    _ => throw new NotImplementedException(),
                });
            else
                Request = await (SearchType switch
                {
                    ItemType.Subject => SearchSubjectAsync(cancellationToken),
                    ItemType.Character => SearchCharacterAsync(cancellationToken),
                    ItemType.Person => SearchPersonAsync(cancellationToken),
                    _ => throw new NotImplementedException(),
                });
        }
        catch (Exception e)
        {
            Trace.TraceError(e.Message);
            Request = null;
        }
    }

    private async Task<object?> SearchSubjectAsync(CancellationToken cancellationToken)
    {
        var request = new SubjectsPostRequestBody()
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
                RatingCount = GetRatingCountFilter(),
            }
        };

        var response = await ApiC.V0.Search.Subjects.PostAsync(request, config =>
        {
            config.Paging(Limit, 0);
        }, cancellationToken);
        if (response == null) return null;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
        return request;
    }
    private async Task SearchSubjectPageAsync(int offset, SubjectsPostRequestBody request, CancellationToken cancellationToken)
    {
        var response = await ApiC.V0.Search.Subjects.PostAsync(request, config =>
        {
            config.Paging(Limit, offset);
        }, cancellationToken);
        if (response == null) return;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
    }

    private async Task<object?> SearchPersonAsync(CancellationToken cancellationToken)
    {
        var request = new PersonsPostRequestBody()
        {
            Keyword = Keyword,
            Filter = new PersonsPostRequestBody_filter() { Career = GetCareerFilter() }
        };

        var response = await ApiC.V0.Search.Persons.PostAsync(request, config =>
        {
            config.Paging(Limit, 0);
        }, cancellationToken);
        if (response == null) return null;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
        return request;
    }
    private async Task SearchPersonPageAsync(int offset, PersonsPostRequestBody request, CancellationToken cancellationToken)
    {
        var response = await ApiC.V0.Search.Persons.PostAsync(request, config =>
        {
            config.Paging(Limit, offset);
        }, cancellationToken);
        if (response == null) return;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
    }

    private async Task<object?> SearchCharacterAsync(CancellationToken cancellationToken)
    {
        var request = new CharactersPostRequestBody()
        {
            Keyword = Keyword,
            Filter = new CharactersPostRequestBody_filter() { Nsfw = Nsfw }
        };

        var response = await ApiC.V0.Search.Characters.PostAsync(request, config =>
        {
            config.Paging(Limit, 0);
        }, cancellationToken);
        if (response == null) return null;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
        return request;
    }
    private async Task SearchCharacterPageAsync(int offset, CharactersPostRequestBody request, CancellationToken cancellationToken)
    {
        var response = await ApiC.V0.Search.Characters.PostAsync(request, config =>
        {
            config.Paging(Limit, offset);
        }, cancellationToken);
        if (response == null) return;

        UpdateItems(response);
        PageNavigator.UpdatePageInfo(response);
    }

    public override int Limit => SettingProvider.Current.SearchPageSize;

    [Reactive] public partial string? SubjectErrorMessage { get; set; }
    [Reactive] public partial string? PersonErrorMessage { get; set; }
    [Reactive] public partial string? CharacterErrorMessage { get; set; }
    [Reactive] public partial string? Keyword { get; set; }
    [Reactive] public partial ItemType SearchType { get; set; }
    [Reactive] public partial SubjectsPostRequestBody_sort? Sort { get; set; }
    [Reactive] public partial List<SubjectTypeOptionViewModel> Type { get; set; }
    [Reactive] public partial List<PersonCareerOptionViewModel> Career { get; set; }
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
    [Reactive] public partial double RatingLowerLimit { get; set; }
    [Reactive] public partial double RatingUpperLimit { get; set; }
    [Reactive] public partial bool IsRatingCountEnabled { get; set; }
    [Reactive] public partial int? RatingCountLowerLimit { get; set; }
    [Reactive] public partial int? RatingCountUpperLimit { get; set; }
    [Reactive] public partial bool? Nsfw { get; set; }
    [Reactive] public partial object? Request { get; set; }

    [Reactive] public partial ReactiveCommand<Unit, Unit> SearchCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit> AddTagCommand { get; set; }
    [Reactive] public partial ReactiveCommand<string, Unit> DelTagCommand { get; set; }
    [Reactive] public partial ReactiveCommand<Unit, Unit> AddMetaTagCommand { get; set; }
    [Reactive] public partial ReactiveCommand<string, Unit> DelMetaTagCommand { get; set; }

    public List<int?>? GetTypeFilter()
    {
        var list = Type.Where(x => x.IsSelected).Select(x => (int?)x.SubjectType).ToList();
        if (list.Count > 0) return list;
        else return null;
    }
    public List<string>? GetCareerFilter()
    {
        var list = Career.Where(x => x.IsSelected).Select(x => x.PersonCareer.ToString().ToLower()).ToList();
        if (list.Count > 0) return list;
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
        else return [$"{SearchFilterRelation.GreaterThanOrEqualTo.ToSymbol()}{RatingLowerLimit}",
                     $"{SearchFilterRelation.LessThanOrEqualTo.ToSymbol()}{RatingUpperLimit}"];
    }
    public List<string>? GetRatingCountFilter()
    {
        if (!IsRatingCountEnabled) return null;
        var list = new List<string>();
        if (RatingCountLowerLimit.HasValue)
            list.Add($"{SearchFilterRelation.GreaterThanOrEqualTo.ToSymbol()}{RatingCountLowerLimit.Value}");
        if (RatingCountUpperLimit.HasValue)
            list.Add($"{SearchFilterRelation.LessThanOrEqualTo.ToSymbol()}{RatingCountUpperLimit.Value}");
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
    public bool IsFilterValid => !IsSearchingSubject || IsAirDateValid && IsRankValid && IsRatingValid && IsRatingCountValid;

    public bool IsAirDateValid => !IsAirDateEnabled || AirDateEnd == null || AirDateStart == null || AirDateEnd >= AirDateStart;
    public bool IsRankValid => !IsRankEnabled || RankUpperLimit == null || RankLowerLimit == null || RankUpperLimit >= RankLowerLimit;
    public bool IsRatingValid => !IsRatingEnabled || RatingUpperLimit >= RatingLowerLimit;
    public bool IsRatingCountValid => !IsRatingCountEnabled || RatingCountUpperLimit == null || RatingCountLowerLimit == null || RatingCountUpperLimit >= RatingCountLowerLimit;

    public bool IsSearchingSubject => SearchType == ItemType.Subject;
    public bool IsSearchingPerson => SearchType == ItemType.Person;
    public bool IsSearchingCharacter => SearchType == ItemType.Character;
}

public partial class SubjectTypeOptionViewModel : ViewModelBase
{
    public static List<SubjectTypeOptionViewModel> GetList()
        => [.. Enum.GetValues<SubjectType>().Select(x => new SubjectTypeOptionViewModel(x, x.GetNameCn()))];

    private SubjectTypeOptionViewModel(SubjectType v, string? name = null)
    {
        SubjectType = v;
        DisplayName = name ?? v.ToString();
    }

    [Reactive] public partial SubjectType SubjectType { get; set; }
    [Reactive] public partial string DisplayName { get; set; }
    [Reactive] public partial bool IsSelected { get; set; }
}
public partial class PersonCareerOptionViewModel : ViewModelBase
{
    public static List<PersonCareerOptionViewModel> GetList()
        => [.. Enum.GetValues<PersonCareer>().Select(x => new PersonCareerOptionViewModel(x, x.ToStringSC()))];

    private PersonCareerOptionViewModel(PersonCareer v, string? name = null)
    {
        PersonCareer = v;
        DisplayName = name ?? v.ToString();
    }

    [Reactive] public partial PersonCareer PersonCareer { get; set; }
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
