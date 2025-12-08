using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Common;
using BangumiNet.Common.Attributes;
using BangumiNet.Common.Extras;
using BangumiNet.Converters;
using BangumiNet.Models;
using System.Reactive.Linq;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class CharacterViewModel : ItemViewModelBase
{
    public CharacterViewModel(Character character)
    {
        Source = character;
        if (character.BirthDay != null || character.BirthMon != null || character.BirthYear != null)
        {
            DateOnly bd = new();
            if (character.BirthYear != null) bd = bd.AddYears((int)character.BirthYear - 1);
            if (character.BirthMon != null) bd = bd.AddMonths((int)character.BirthMon - 1);
            if (character.BirthDay != null) bd = bd.AddDays((int)character.BirthDay - 1);
            Birthday = bd;
        }
        BloodType = (BloodType?)character.BloodType;
        Gender = EnumExtensions.TryParseGender(character.Gender);
        GenderString = character.Gender;
        Id = character.Id;
        Images = character.Images;
        IsLocked = character.Locked ?? false;
        Infobox = character.Infobox?.Select(x => new InfoboxItemViewModel(x)).ToObservableCollection();
        Name = character.Name;
        CommentCount = character.Stat?.Comments;
        CollectionTotal = character.Stat?.Collects;
        Summary = character.Summary;
        Type = (CharacterType?)character.Type;
        IsNsfw = (bool?)character.AdditionalData["nsfw"] ?? false;

        Init();
    }
    public CharacterViewModel(RelatedCharacter character)
    {
        Source = character;
        Id = character.Id;
        Relation = character.Relation;
        RelationItems = new() { SubjectViewModels = character.Actors?.Select<Person, ViewModelBase>(x => new PersonViewModel(x)).ToObservableCollection() };
        Images = character.Images;
        Name = character.Name;
        Type = (CharacterType?)character.Type;

        Init();
    }
    public CharacterViewModel(PersonCharacter character)
    {
        Source = character;
        Id = character.Id;
        Relation = $"{character.Staff} · {NameCnCvt.Convert(character.SubjectName, character.SubjectNameCn)}";
        Images = character.Images;
        Name = character.Name;
        Type = (CharacterType?)character.Type;
        RelationItems = new()
        {
            SubjectViewModels = [new SubjectViewModel(new SubjectBasic
            {
                Id = character.SubjectId,
                Name = character.SubjectName,
                NameCn = character.SubjectNameCn,
                Type = (SubjectType?)character.SubjectType
            })],
        };

        Init();
    }
    public CharacterViewModel(UserCharacterCollection character)
    {
        Source = character;
        CollectionTime = character.CreatedAt;
        Id = character.Id;
        Name = character.Name;
        Type = (CharacterType?)character.Type;
        Images = character.Images;

        Init();
    }
    public CharacterViewModel(Api.P1.Models.SlimCharacter character)
    {
        Source = character;

        Id = character.Id;
        Images = character.Images;
        IsLocked = character.Lock ?? false;
        IsNsfw = character.Nsfw ?? false;
        Name = character.Name;
        NameCn = character.NameCN;
        CommentCount = character.Comment;
        Type = (CharacterType?)character.Role;
        Info = character.Info;

        Init();
    }
    public CharacterViewModel(Api.P1.Models.Character character)
    {
        Source = character;

        Id = character.Id;
        Images = character.Images;
        IsLocked = character.Lock ?? false;
        IsNsfw = character.Nsfw ?? false;
        Name = character.Name;
        NameCn = character.NameCN;
        CommentCount = character.Comment;
        Type = (CharacterType?)character.Role;
        Info = character.Info;
        CollectionTime = CommonUtils.ParseBangumiTime(character.CollectedAt);
        CollectionTotal = character.Collects;
        Redirect = character.Redirect;
        Summary = character.Summary;
        Infobox = character.Infobox?.Select(p => new InfoboxItemViewModel(p)).ToObservableCollection();

        Init();
    }
    public static CharacterViewModel Init(Api.P1.Models.SubjectCharacter subjectCharacter)
        => new(subjectCharacter.Character!)
        {
            RelationItems = new() { SubjectViewModels = subjectCharacter.Actors?.Select<Api.P1.Models.SlimPerson, ViewModelBase>(x => new PersonViewModel(x)).ToObservableCollection() },
            Order = subjectCharacter.Order,
            Relation = ((CharacterRole?)subjectCharacter.Type)?.GetNameCn(),
        };
    public static CharacterViewModel Init(Api.P1.Models.PersonCharacter personCharacter)
        => new(personCharacter.Character!)
        {
            Relation = string.Join('\n', personCharacter.Relations?.Select(x => $"{((CharacterRole?)x.Type)?.GetNameCn()}·{NameCnCvt.Convert(x.Subject)}") ?? []),
        };
    private void Init()
    {
        ItemType = ItemType.Character;

        SubjectBadgeListViewModel = new(RelatedItemType.CharacterCast, ItemType, Id);
        PersonBadgeListViewModel = new(RelatedItemType.Person, ItemType, Id);
        CommentListViewModel = new(ItemType, Id);
        RevisionListViewModel = new(this);

        OpenInBrowserCommand = ReactiveCommand.Create(() => CommonUtils.OpenUrlInBrowser(UrlProvider.BangumiTvCharacterUrlBase + Id));
        CollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(true), this.WhenAnyValue(x => x.IsCollected).Select(x => !x));
        UncollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(false), this.WhenAnyValue(x => x.IsCollected));

        this.WhenAnyValue(x => x.CollectionTime).Subscribe(x => this.RaisePropertyChanged(nameof(IsCollected)));

        Title = $"{NameCnCvt.Convert(this) ?? $"角色 {Id}"} - {Title}";
    }

    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? Info { get; set; }
    [Reactive] public partial DateOnly? Birthday { get; set; }
    [Reactive] public partial Gender? Gender { get; set; }
    [Reactive] public partial string? GenderString { get; set; }
    [Reactive] public partial BloodType? BloodType { get; set; }
    [Reactive] public partial CharacterType? Type { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImagesGrid? Images { get; set; }
    [Reactive] public partial bool IsLocked { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }

    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial RelatedItemListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial RelatedItemListViewModel? PersonBadgeListViewModel { get; set; }
    [Reactive] public partial CommentListViewModel? CommentListViewModel { get; set; }

    [Reactive] public partial DateTimeOffset? CollectionTime { get; set; }

    public bool IsCollected => CollectionTime != null;

    public ICommand? CollectCommand { get; private set; }
    public ICommand? UncollectCommand { get; private set; }

    public bool IsFull => Source is Api.P1.Models.Character;

    public async Task UpdateCollection(bool target)
    {
        if (Id == null) return;
        var result = await ApiC.UpdateCollection(ItemType.Character, (int)Id, target);
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
                MessageWindow.Show("角色不存在。");
                break;
            default: break;
        }
    }
}
