using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BangumiNet.ViewModels;

public partial class CharacterViewModel : ViewModelBase
{
    public CharacterViewModel(Character character)
    {
        if (character.BirthDay != null || character.BirthMon != null || character.BirthYear != null)
        {
            DateOnly bd = new();
            if (character.BirthYear != null) bd = bd.AddYears((int)character.BirthYear);
            if (character.BirthMon != null) bd = bd.AddMonths((int)character.BirthMon);
            if (character.BirthDay != null) bd = bd.AddDays((int)character.BirthDay);
            Birthday = bd;
        }
        BloodType = (BloodType?)character.BloodType;
        Gender = EnumExtensions.TryParseGender(character.Gender, out string? gStr);
        GenderString = gStr;
        Id = character.Id;
        Images = character.Images;
        IsLocked = character.Locked;
        Infobox = character.Infobox?.Select(x => new InfoboxItemViewModel(x)).ToObservableCollection();
        Name = character.Name;
        CommentCount = character.Stat?.Comments;
        CollectionTotal = character.Stat?.Collects;
        Summary = character.Summary;
        CharacterType = (CharacterType?)character.Type;
        Nsfw = (bool?)character.AdditionalData["nsfw"];
    }

    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial DateOnly? Birthday { get; set; }
    [Reactive] public partial Gender? Gender { get; set; }
    [Reactive] public partial string? GenderString { get; set; }
    [Reactive] public partial BloodType? BloodType { get; set; }
    [Reactive] public partial CharacterType? CharacterType { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial bool? IsLocked { get; set; }
    [Reactive] public partial bool? Nsfw { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);
}
