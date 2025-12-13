using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Api.P1.P1.Wiki.Subjects.Item.Covers;
using BangumiNet.Models;

namespace BangumiNet.ViewModels;

public partial class PhotoViewModel : ItemViewModelBase
{
    public PhotoViewModel(BlogPhoto photo)
    {
        ItemType = ItemType.Photo;
        Id = photo.Id;
        Vote = photo.Vote;
        Target = photo.Target;
        Images = new ImageSet
        {
            Grid = photo.Icon,
            Large = $"https://lain.bgm.tv/pic/photo/l/{photo.Target}",
        };
        CreationTime = CommonUtils.ParseBangumiTime(photo.CreatedAt);
    }
    public PhotoViewModel(CoversGetResponse_current cover)
    {
        ItemType = ItemType.Cover;
        IsCurrent = true;
        Id = cover.Id;
        Images = new ImageSet
        {
            Grid = cover.Thumbnail,
            Large = cover.Raw,
        };
    }
    public PhotoViewModel(CoversGetResponse_covers cover)
    {
        ItemType = ItemType.Cover;
        IsVoted = cover.Voted ?? false;
        Id = cover.Id;
        Images = new ImageSet
        {
            Grid = cover.Thumbnail,
            Large = cover.Raw,
        };
        if (cover.Creator != null)
            User = new(cover.Creator);
    }

    [Reactive] public partial string? Target { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial int? Vote { get; set; }
    [Reactive] public partial bool IsVoted { get; set; }
    [Reactive] public partial bool IsCurrent { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
    [Reactive] public partial UserViewModel? User { get; set; }
}
