using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Views;
using DynamicData.Binding;
using Microsoft.Kiota.Abstractions.Serialization;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Net;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BangumiNet.ViewModels;

public partial class PersonViewModel : ViewModelBase
{
    public PersonViewModel(Person person)
    {
        Source = person;
        Id = person.Id;
        Name = person.Name;
        Careers = person.Career.ToObservableCollection();
        IsLocked = person.Locked;
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
            if (Common.NumberToInt(birthYear) is int year) bd = bd.AddYears(year);
            if (Common.NumberToInt(birthMon) is int mon) bd = bd.AddMonths(mon);
            if (Common.NumberToInt(birthDay) is int day) bd = bd.AddDays(day);
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

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new PersonView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvPersonUrlBase + Id));

        this.WhenAnyValue(x => x.Careers).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(CareerString));
            Careers?.ObserveCollectionChanges().Subscribe(x => this.RaisePropertyChanged(nameof(CareerString)));
        });
    }
    public PersonViewModel(PersonDetail person)
    {
        Source = person;
        Id = person.Id;
        Name = person.Name;
        Careers = person.Career.ToObservableCollection();
        IsLocked = person.Locked;
        Images = person.Images;
        Summary = person.Summary;
        Type = (PersonType?)person.Type;

        if (person.AdditionalData.TryGetValue("blood_type", out var bt) && bt is int bloodType)
            BloodType = (BloodType?)bloodType;
        person.AdditionalData.TryGetValue("birth_year", out var birthYear);
        person.AdditionalData.TryGetValue("birth_mon", out var birthMon);
        person.AdditionalData.TryGetValue("birth_day", out var birthDay);
        if (birthYear != null || birthMon != null || birthDay != null)
        {
            DateOnly bd = new();
            if (Common.NumberToInt(birthYear) is int year) bd = bd.AddYears(year);
            if (Common.NumberToInt(birthMon) is int mon) bd = bd.AddMonths(mon);
            if (Common.NumberToInt(birthDay) is int day) bd = bd.AddDays(day);
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

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new PersonView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvPersonUrlBase + Id));

        this.WhenAnyValue(x => x.Careers).Subscribe(x =>
        {
            this.RaisePropertyChanged(nameof(CareerString));
            Careers?.ObserveCollectionChanges().Subscribe(x => this.RaisePropertyChanged(nameof(CareerString)));
        });
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial string? ShortSummary { get; set; }
    [Reactive] public partial bool? IsLocked { get; set; }
    [Reactive] public partial DateOnly? Birthday { get; set; }
    [Reactive] public partial PersonType? Type { get; set; }
    [Reactive] public partial BloodType? BloodType { get; set; }
    [Reactive] public partial Gender? Gender { get; set; }
    [Reactive] public partial string? GenderString { get; set; }
    [Reactive] public partial ObservableCollection<PersonCareer?>? Careers { get; set; }
    [Reactive] public partial ObservableCollection<InfoboxItemViewModel>? Infobox { get; set; }
    [Reactive] public partial IImages? Images { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchGoogleCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }

    public string? CareerString => Careers?.Where(x => x is not null).Aggregate("", (a, b) => $"{a}{b?.ToStringSC()} ");
}
