using BangumiNet.Common.Models;

namespace BangumiNet.Common;

public static partial class Config
{
    public static TimelineSource[] TimelineSources { get; } = [
        new TimelineSource { Id = 0, Name = "web" },
        new TimelineSource { Id = 1, Name = "mobile" },
        new TimelineSource { Id = 2, Name = "onAir", Url = "https://bgm.tv/onair" },
        new TimelineSource { Id = 3, Name = "inTouch", Url = "https://netaba.re/" },
        new TimelineSource { Id = 4, Name = "Windows Phone 7", Url = "https://www.windowsphone.com/zh-CN/apps/14b39a30-7e5a-4427-88ed-fa40d7e841c1" },
        new TimelineSource { Id = 5, Name = "API", Url = "https://bgm.tv/group/topic/366561" },
        new TimelineSource { Id = 6, Name = "next", Url = "https://next.bgm.tv" },
        new TimelineSource { Id = 7, Name = "Chobits iOS", Url = "https://bgm.tv/group/topic/417148", AppID = "bgm3041662232881a48c" },
    ];
}
