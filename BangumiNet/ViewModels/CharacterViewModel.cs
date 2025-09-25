using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Converters;
using BangumiNet.Models;
using BangumiNet.Views;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class CharacterViewModel : ViewModelBase
{
    public CharacterViewModel(Character character)
    {
        Source = character;
        if (character.BirthDay != null || character.BirthMon != null || character.BirthYear != null)
        {
            DateOnly bd = new();
            if (character.BirthYear != null) bd = bd.AddYears((int)character.BirthYear);
            if (character.BirthMon != null) bd = bd.AddMonths((int)character.BirthMon);
            if (character.BirthDay != null) bd = bd.AddDays((int)character.BirthDay);
            Birthday = bd;
        }
        BloodType = (BloodType?)character.BloodType;
        Gender = EnumExtensions.TryParseGender(character.Gender);
        GenderString = character.Gender;
        Id = character.Id;
        Images = character.Images;
        IsLocked = character.Locked;
        Infobox = character.Infobox?.Select(x => new InfoboxItemViewModel(x)).ToObservableCollection();
        Name = character.Name;
        CommentCount = character.Stat?.Comments;
        CollectionTotal = character.Stat?.Collects;
        Summary = character.Summary;
        Type = (CharacterType?)character.Type;
        IsNsfw = (bool?)character.AdditionalData["nsfw"];

        Init();
    }
    public CharacterViewModel(RelatedCharacter character)
    {
        Source = character;
        Id = character.Id;
        Relation = character.Relation;
        PersonListViewModel = new() { PersonViewModels = character.Actors?.Select(x => new PersonViewModel(x, fromRelation: true)).ToObservableCollection() };
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
    public void Init()
    {
        SubjectBadgeListViewModel = new(ItemType.Character, Id);
        PersonBadgeListViewModel = new(ItemType.Character, Id);

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new CharacterView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvCharacterUrlBase + Id));
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial DateOnly? Birthday { get; set; }
    [Reactive] public partial Gender? Gender { get; set; }
    [Reactive] public partial string? GenderString { get; set; }
    [Reactive] public partial BloodType? BloodType { get; set; }
    [Reactive] public partial CharacterType? Type { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial bool? IsLocked { get; set; }
    [Reactive] public partial bool? IsNsfw { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }
    [Reactive] public partial string? Relation { get; set; }
    [Reactive] public partial PersonListViewModel? PersonListViewModel { get; set; }
    [Reactive] public partial SubjectBadgeListViewModel? SubjectBadgeListViewModel { get; set; }
    [Reactive] public partial PersonBadgeListViewModel? PersonBadgeListViewModel { get; set; }
    [Reactive] public partial SubjectViewModel? CharacterSubjectViewModel { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchGoogleCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }

    public bool IsFull => Source is Character;
}
