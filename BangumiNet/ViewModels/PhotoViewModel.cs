using BangumiNet.Api.Interfaces;
using BangumiNet.Api.P1.Models;
using BangumiNet.Models;

namespace BangumiNet.ViewModels;

public partial class PhotoViewModel : ItemViewModelBase
{
    public PhotoViewModel(BlogPhoto photo)
    {
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

    [Reactive] public partial string? Target { get; set; }
    [Reactive] public partial IImages? Images { get; set; }
    [Reactive] public partial int? Vote { get; set; }
    [Reactive] public partial DateTimeOffset? CreationTime { get; set; }
}
