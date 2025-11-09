using Avalonia.Media.Imaging;
using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;

namespace BangumiNet.ViewModels;

public partial class GroupViewModel : ItemViewModelBase
{
    public GroupViewModel(SlimGroup group)
    {
        Source = group;
        Id = group.Id;
        Name = group.Title;
        Groupname = group.Name;
        Images = group.Icon;
        IsNsfw = group.Nsfw ?? false;
        Accessible = group.Accessible ?? true;
        MemberCount = group.Members;
        CreationTime = Common.ParseBangumiTime(group.CreatedAt);

        Init();
    }
    public void Init()
    {
        ItemTypeEnum = ItemType.Group;
        Title = $"{Name} - {Title}";
        SearchWebCommand = ReactiveCommand.Create(() => Common.SearchWeb(Name));
        OpenInBrowserCommand = ReactiveCommand.Create(() => Common.OpenUrlInBrowser(UrlProvider.BangumiTvGroupUrlBase + Groupname));
    }

    [Reactive] public partial object? Source { get; set; }
    [Reactive] public partial string? Groupname { get; set; }
    [Reactive] public partial bool IsNsfw { get; set; }
    [Reactive] public partial bool Accessible { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial int? MemberCount { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }

    public Task<Bitmap?> ImageSmall => ApiC.GetImageAsync(Images?.Small);
    public Task<Bitmap?> ImageMedium => ApiC.GetImageAsync(Images?.Medium);
    public Task<Bitmap?> ImageLarge => ApiC.GetImageAsync(Images?.Large);
}
