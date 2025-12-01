using BangumiNet.Common.Attributes;

namespace BangumiNet.Common;

public enum TimelineSource
{
    [SourceInfo(Name = "web")]
    Web = 0,

    [SourceInfo(Name = "mobile")]
    Mobile = 1,

    [SourceInfo(
        Name = "onAir",
        Url = "https://bgm.tv/onair")]
    OnAir = 2,

    [SourceInfo(
        Name = "inTouch",
        Url = "https://netaba.re/")]
    InTouch = 3,

    [SourceInfo(
        Name = "Windows Phone 7",
        Url = "https://www.windowsphone.com/zh-CN/apps/14b39a30-7e5a-4427-88ed-fa40d7e841c1")]
    WindowPhone7 = 4,

    [SourceInfo(
        Name = "API",
        Url = "https://bgm.tv/group/topic/366561")]
    API = 5,

    [SourceInfo(
        Name = "next",
        Url = "https://next.bgm.tv")]
    Next = 6,

    [SourceInfo(
        Name = "Chobits iOS",
        Url = "https://bgm.tv/group/topic/417148",
        AppId = "bgm3041662232881a48c")]
    ChobitsIOS = 7,
}
