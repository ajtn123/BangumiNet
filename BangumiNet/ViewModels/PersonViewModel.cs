using Avalonia.Media.Imaging;
using BangumiNet.Api.ExtraEnums;
using BangumiNet.Api.Helpers;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.V0.Models;
using BangumiNet.Views;
using Microsoft.Kiota.Abstractions.Serialization;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using System.Collections.ObjectModel;
using System.Net;
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
        Summary = person.ShortSummary;
        Type = (PersonType?)person.Type;
        if (person.AdditionalData.TryGetValue("stat", out var statO) && statO is UntypedObject statUO && statUO.ToObject() is IDictionary<string, object?> stat)
        {
            stat.TryGetValue("collects", out var collectsNode);
            stat.TryGetValue("comments", out var commentsNode);
            CollectionTotal = (int?)collectsNode;
            CommentCount = (int?)commentsNode;
        }

        OpenInNewWindowCommand = ReactiveCommand.Create(() => new SecondaryWindow() { Content = new PersonView() { DataContext = this } }.Show());
        SearchGoogleCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.GoogleQueryBase + WebUtility.UrlEncode(Name)));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvPersonUrlBase + Id));
    }

    [Reactive] public partial Person Source { get; set; }
    [Reactive] public partial int? Id { get; set; }
    [Reactive] public partial int? CommentCount { get; set; }
    [Reactive] public partial int? CollectionTotal { get; set; }
    [Reactive] public partial string? Name { get; set; }
    [Reactive] public partial string? Summary { get; set; }
    [Reactive] public partial bool? IsLocked { get; set; }
    [Reactive] public partial PersonType? Type { get; set; }
    [Reactive] public partial ObservableCollection<PersonCareer?>? Careers { get; set; }
    [Reactive] public partial IImages? Images { get; set; }

    public Task<Bitmap?> ImageGrid => ApiC.GetImageAsync(Images?.Grid);
    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);

    public ICommand? OpenInNewWindowCommand { get; private set; }
    public ICommand? SearchGoogleCommand { get; private set; }
    public ICommand? OpenInBrowserCommand { get; private set; }
}
