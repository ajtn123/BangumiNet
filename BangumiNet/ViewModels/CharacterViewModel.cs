using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
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
        PersonListViewModel = new() { SubjectViewModels = character.Actors?.Select<Person, ViewModelBase>(x => new PersonViewModel(x, fromRelation: true)).ToObservableCollection() };
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
        CharacterSubjectViewModel = new(new SubjectBasic()
        {
            Id = character.SubjectId,
            Name = character.SubjectName,
            NameCn = character.SubjectNameCn,
            Type = (SubjectType?)character.SubjectType
        });

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
    public void Init()
    {
        ItemTypeEnum = ItemType.Character;

        SubjectBadgeListViewModel = new(ItemTypeEnum, Id);
        PersonBadgeListViewModel = new(ItemTypeEnum, Id);
        RevisionListViewModel = new(this);

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new CharacterView() { DataContext = this } }.Show());
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvCharacterUrlBase + Id));
        CollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(true), this.WhenAnyValue(x => x.IsCollected).Select(x => !x));
        UncollectCommand = ReactiveCommand.CreateFromTask(async () => await UpdateCollection(false), this.WhenAnyValue(x => x.IsCollected));

        this.WhenAnyValue(x => x.CollectionTime).Subscribe(x => this.RaisePropertyChanged(nameof(IsCollected)));

        Title = $"{Name ?? $"角色 {Id}"} - {Title}";
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? Summary { get; set; }
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
    [Reactive] public partial SubjectListViewModel? PersonListViewModel { get; set; }
    [Reactive] public partial SubjectBadgeListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial PersonBadgeListViewModel? PersonBadgeListViewModel { get; set; }
    [Reactive] public partial SubjectViewModel? CharacterSubjectViewModel { get; set; }

    [Reactive] public partial DateTimeOffset? CollectionTime { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public bool IsCollected => CollectionTime != null;

    public ICommand? CollectCommand { get; private set; }
    public ICommand? UncollectCommand { get; private set; }

    public bool IsFull => Source is Character;

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
                MessageWindow.ShowMessage("非法 ID。");
                break;
            case 401:
                MessageWindow.ShowMessage("未登录。");
                break;
            case 404:
                MessageWindow.ShowMessage("角色不存在。");
                break;
            default: break;
        }
    }
}
