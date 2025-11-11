using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using BangumiNet.Models;
using DynamicData.Binding;
using Microsoft.Kiota.Abstractions.Serialization;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class PersonViewModel : ItemViewModelBase
{
    public PersonViewModel(Person person, bool fromRelation = false)
    {
        Source = person;
        Id = person.Id;
        Name = person.Name;
        Careers = person.Career?.Select(static c => c?.ToStringSC()).ToObservableCollection()!;
        IsLocked = person.Locked ?? false;
        Images = person.Images;
        ShortSummary = person.ShortSummary;
        Type = (PersonType?)person.Type;
        FromRelation = fromRelation;

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
            if (Common.ToInt32(birthYear) is int year) bd = bd.AddYears(year - 1);
            if (Common.ToInt32(birthMon) is int mon) bd = bd.AddMonths(mon - 1);
            if (Common.ToInt32(birthDay) is int day) bd = bd.AddDays(day - 1);
            Birthday = bd;
        }
        if (person.AdditionalData.TryGetValue("gender", out var gd) && gd is string gender)
        {
            GenderString = gender;
            Gender = EnumExtensions.TryParseGender(gender);
        }
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
        Gender = EnumExtensions.TryParseGender(person.Gender);
        GenderString = person.Gender;
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
        CharacterSubjectViewModel = new(new SubjectBasic()
        {
            Id = person.SubjectId,
            Name = person.SubjectName,
            NameCn = person.SubjectNameCn,
            Type = (SubjectType?)person.SubjectType
        });

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
        Careers = person.Career?.Select(static c => EnumExtensions.ParsePersonCareer(c)?.ToStringSC() ?? c).ToObservableCollection();

        Init();
    }
    public void Init()
    {
        ItemTypeEnum = ItemType.Person;

        SubjectBadgeListViewModel = new(ItemTypeEnum, Id);
        CharacterBadgeListViewModel = new(ItemTypeEnum, Id);
        CommentListViewModel = new(ItemTypeEnum, Id);
        RevisionListViewModel = new(this);

        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvPersonUrlBase + Id));
        CollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(true), this.WhenAnyValue(x => x.IsCollected).Select(x => !x));
        UncollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(false), this.WhenAnyValue(x => x.IsCollected));

        this.WhenAnyValue(x => x.CollectionTime).Subscribe(x => this.RaisePropertyChanged(nameof(IsCollected)));
        this.WhenAnyValue(x => x.Careers).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(CareerString));
            Careers?.ObserveCollectionChanges().Subscribe(x => this.RaisePropertyChanged(nameof(CareerString)));
        });

        Title = $"{NameCnCvt.Convert(this) ?? $"人物 {Id}"} - {Title}";
    }

    [Reactive] public partial object? Source { get; set; }
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
    [Reactive] public partial Gender? Gender { get; set; }
    [Reactive] public partial string? GenderString { get; set; }
    [Reactive] public partial ObservableCollection<string>? Careers { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImagesGrid? Images { get; set; }

    [Reactive] public partial bool FromRelation { get; set; }
    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial string? Eps { get; set; }
    [Reactive] public partial SubjectBadgeListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial CharacterBadgeListViewModel? CharacterBadgeListViewModel { get; set; }
    [Reactive] public partial SubjectViewModel? CharacterSubjectViewModel { get; set; }
    [Reactive] public partial CommentListViewModel? CommentListViewModel { get; set; }

    [Reactive] public partial DateTimeOffset? CollectionTime { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public bool IsCollected => CollectionTime != null;

    public ICommand? CollectCommand { get; private set; }
    public ICommand? UncollectCommand { get; private set; }

    public string? CareerString => Careers?.Where(x => x is not null).Aggregate("", (a, b) => $"{a}{b} ");
    public bool IsFull => !FromRelation && Source is Person or PersonDetail;

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
                MessageWindow.ShowMessage("非法 ID。");
                break;
            case 401:
                MessageWindow.ShowMessage("未登录。");
                break;
            case 404:
                MessageWindow.ShowMessage("人物不存在。");
                break;
            default: break;
        }
    }
}
