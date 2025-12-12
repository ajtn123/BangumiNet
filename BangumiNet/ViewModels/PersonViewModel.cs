using BangumiNet.Api.Extensions;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Common;
using BangumiNet.Common.Extras;
using BangumiNet.Converters;
using BangumiNet.Models;
using DynamicData.Binding;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class PersonViewModel : ItemViewModelBase
{
    public PersonViewModel(Person person)
    {
        Source = person;
        Id = person.Id;
        Name = person.Name;
        Careers = person.Career?.Select(static c => c?.ToStringSC()).ToObservableCollection()!;
        IsLocked = person.Locked ?? false;
        Images = person.Images;
        ShortSummary = person.ShortSummary;
        Type = (PersonType?)person.Type;

        if (person.AdditionalData.TryGetValue("summary", out var summary))
            Summary = summary.ToString();
        if (person.AdditionalData.TryGetValue("blood_type", out var bt) && bt is int bloodType)
            BloodType = (BloodType?)bloodType;
        person.AdditionalData.TryGetValue("birth_year", out var birthYear);
        person.AdditionalData.TryGetValue("birth_mon", out var birthMon);
        person.AdditionalData.TryGetValue("birth_day", out var birthDay);
        if (birthYear != null || birthMon != null || birthDay != null)
        {
            DateOnly bd = new();
            if (CommonUtils.ToInt32(birthYear) is int year) bd = bd.AddYears(year - 1);
            if (CommonUtils.ToInt32(birthMon) is int mon) bd = bd.AddMonths(mon - 1);
            if (CommonUtils.ToInt32(birthDay) is int day) bd = bd.AddDays(day - 1);
            Birthday = bd;
        }
        if (person.AdditionalData.TryGetValue("gender", out var gd) && gd is string gender)
            Gender = gender;
        if (person.AdditionalData.TryGetValue("stat", out var statO) && statO is UntypedObject statUO && statUO.ToObject() is IDictionary<string, object?> stat)
        {
            stat.TryGetValue("collects", out var collectsNode);
            stat.TryGetValue("comments", out var commentsNode);
            CollectionTotal = (int?)collectsNode;
            CommentCount = (int?)commentsNode;
        }
        if (person.AdditionalData.TryGetValue("infobox", out var ib) && ib is UntypedArray ua && ua.ToObject() is List<object?> list)
            Infobox = list.Select(x => x is not Dictionary<string, object?> dict ? null : new InfoboxItemViewModel(dict))
                .Where(y => y is not null).ToObservableCollection()!;

        Init();
    }
    public PersonViewModel(PersonDetail person)
    {
        Source = person;
        Id = person.Id;
        Name = person.Name;
        Careers = person.Career?.Select(static c => c?.ToStringSC()).ToObservableCollection()!;
        IsLocked = person.Locked ?? false;
        Images = person.Images;
        Summary = person.Summary;
        Type = (PersonType?)person.Type;
        BloodType = (BloodType?)person.BloodType;
        Gender = person.Gender;
        CommentCount = person.Stat?.Comments;
        CollectionTotal = person.Stat?.Collects;
        Infobox = person.Infobox?.Select(x => new InfoboxItemViewModel(x)).ToObservableCollection();
        if (person.BirthYear != null || person.BirthMon != null || person.BirthDay != null)
        {
            DateOnly bd = new();
            if (person.BirthYear != null) bd = bd.AddYears((int)person.BirthYear - 1);
            if (person.BirthMon != null) bd = bd.AddMonths((int)person.BirthMon - 1);
            if (person.BirthDay != null) bd = bd.AddDays((int)person.BirthDay - 1);
            Birthday = bd;
        }

        Init();
    }
    public PersonViewModel(RelatedPerson person)
    {
        Source = person;
        Name = person.Name;
        Relation = person.Relation;
        Careers = person.Career?.Select(static c => c?.ToStringSC()).ToObservableCollection()!;
        Type = (PersonType?)person.Type;
        Id = person.Id;
        Eps = person.Eps;
        Images = person.Images;

        Init();
    }
    public PersonViewModel(CharacterPerson person)
    {
        Source = person;
        Name = person.Name;
        Relation = $"{person.Staff} · {NameCnCvt.Convert(person.SubjectName, person.SubjectNameCn)}";
        Type = (PersonType?)person.Type;
        Id = person.Id;
        Images = person.Images;
        RelationItems = new()
        {
            SubjectViewModels = [new SubjectViewModel(new SubjectBasic
            {
                Id = person.SubjectId,
                Name = person.SubjectName,
                NameCn = person.SubjectNameCn,
                Type = (SubjectType?)person.SubjectType
            })],
        };

        Init();
    }
    public PersonViewModel(UserPersonCollection person)
    {
        Source = person;
        Name = person.Name;
        Id = person.Id;
        Images = person.Images;
        Type = (PersonType?)person.Type;
        CollectionTime = person.CreatedAt;
        Careers = person.Career?.Select(static c => c?.ToStringSC()).ToObservableCollection()!;

        Init();
    }
    public PersonViewModel(Api.P1.Models.SlimPerson person)
    {
        Source = person;
        Name = person.Name;
        NameCn = person.NameCN;
        Id = person.Id;
        Images = person.Images;
        IsNsfw = person.Nsfw ?? false;
        IsLocked = person.Lock ?? false;
        Type = (PersonType?)person.Type;
        CommentCount = person.Comment;
        Info = person.Info;
        Careers = person.Career?.Select(static c => Api.ExtraEnums.EnumExtensions.ParsePersonCareer(c)?.ToStringSC() ?? c).ToObservableCollection();

        Init();
    }
    public PersonViewModel(Api.P1.Models.Person person)
    {
        Source = person;
        Name = person.Name;
        NameCn = person.NameCN;
        Id = person.Id;
        Images = person.Images;
        IsNsfw = person.Nsfw ?? false;
        IsLocked = person.Lock ?? false;
        Type = (PersonType?)person.Type;
        CommentCount = person.Comment;
        Info = person.Info;
        Careers = person.Career?.Select(static c => Api.ExtraEnums.EnumExtensions.ParsePersonCareer(c)?.ToStringSC() ?? c).ToObservableCollection();
        CollectionTime = CommonUtils.ParseBangumiTime(person.CollectedAt);
        CollectionTotal = person.Collects;
        Redirect = person.Redirect;
        Summary = person.Summary;
        Infobox = person.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();

        Init();
    }
    public static PersonViewModel Init(Api.P1.Models.SubjectStaff staff)
        => new(staff.Staff!)
        {
            Relation = string.Join(' ', staff.Positions?.Select(x => x.Type?.ToLocalString()) ?? []),
        };
    private void Init()
    {
        ItemType = ItemType.Person;

        SubjectBadgeListViewModel = new(RelatedItemType.PersonWork, ItemType, Id);
        CharacterBadgeListViewModel = new(RelatedItemType.PersonCast, ItemType, Id);
        IndexCardListViewModel = new(RelatedItemType.Index, ItemType, Id);
        CommentListViewModel = new(ItemType, Id);
        RevisionListViewModel = new(this);

        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(UrlProvider.BangumiTvPersonUrlBase + Id));
        CollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(true), this.WhenAnyValue(x => x.IsCollected).Select(x => !x));
        UncollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(false), this.WhenAnyValue(x => x.IsCollected));

        this.WhenAnyValue(x => x.CollectionTime).Subscribe(x => this.RaisePropertyChanged(nameof(IsCollected)));
        this.WhenAnyValue(x => x.Careers).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(CareerString));
            Careers?.ObserveCollectionChanges().Subscribe(x => this.RaisePropertyChanged(nameof(CareerString)));
        });
    }

    [Reactive] public partial int? CommentCount { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? ShortSummary { get; set; }
    [Reactive] public partial string? Info { get; set; }
    [Reactive] public partial bool IsLocked { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial DateOnly? Birthday { get; set; }
    [Reactive] public partial PersonType? Type { get; set; }
    [Reactive] public partial BloodType? BloodType { get; set; }
    [Reactive] public partial string? Gender { get; set; }
    [Reactive] public partial ObservableCollection<string>? Careers { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImagesGrid? Images { get; set; }

    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial string? Eps { get; set; }
    [Reactive] public partial RelatedItemListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? CharacterBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? IndexCardListViewModel { get; set; }
    [Reactive] public partial CommentListViewModel? CommentListViewModel { get; set; }

    [Reactive] public partial DateTimeOffset? CollectionTime { get; set; }

    public bool IsCollected => CollectionTime != null;

    public ICommand? CollectCommand { get; private set; }
    public ICommand? UncollectCommand { get; private set; }

    public string? CareerString => Careers?.Where(x => x is not null).Aggregate("", (a, b) => $"{a}{b} ");
    public bool IsFull => Source is Api.P1.Models.Person;

    public async Task UpdateCollection(bool target)
    {
        if (Id == null) return;
        var result = await ApiC.UpdateCollection(ItemType.Person, (int)Id, target);
        switch (result)
        {
            case 204:
                CollectionTime = target ? DateTimeOffset.Now : null;
                break;
            case 400:
                MessageWindow.Show("非法 ID。");
                break;
            case 401:
                MessageWindow.Show("未登录。");
                break;
            case 404:
                MessageWindow.Show("人物不存在。");
                break;
            default: break;
        }
    }
}
