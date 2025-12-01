using BangumiNet.Common.Attributes;

namespace BangumiNet.Common;

public enum NetworkService
{
    [ServiceInfo(
        Name = "psn",
        Title = "PSN",
        BackgroundColor = "#004AB0",
        Url = "https://www.playstation.com/en-us/my/public-trophies/?onlineId=")]
    PSN = 1,

    [ServiceInfo(
        Name = "live",
        Title = "Xbox Live",
        BackgroundColor = "#107C11",
        Url = "https://account.xbox.com/en-US/Profile?gamerTag=",
        ValidationRegex = @"^[A-Za-z0-90-9]*( )?_?\-?\.?[A-Za-z0-9]*")]
    XboxLive = 2,

    [ServiceInfo(
        Name = "fc",
        Title = "FriendCode",
        BackgroundColor = "#EE1C23",
        ValidationRegex = @"^[0-9]{4}-[0-9]{4}-[0-9]{4}$")]
    FriendCode = 3,

    [ServiceInfo(
        Name = "steam",
        Title = "Steam",
        BackgroundColor = "#171A21",
        Url = "https://steamcommunity.com/id/")]
    Steam = 4,

    [ServiceInfo(
        Name = "battletag",
        Title = "BattleTag",
        BackgroundColor = "#007CAE",
        ValidationRegex = @"^[A-Za-z][A-Za-z0-9]*#[0-9]*$")]
    BattleTag = 5,

    [ServiceInfo(
        Name = "pixiv",
        Title = "Pixiv",
        BackgroundColor = "#0097DC",
        Url = "https://www.pixiv.net/users/",
        ValidationRegex = @"^[0-9]*$")]
    Pixiv = 6,

    [ServiceInfo(
        Name = "github",
        Title = "GitHub",
        BackgroundColor = "#333",
        Url = "https://github.com/")]
    GitHub = 7,

    [ServiceInfo(
        Name = "twitter",
        Title = "Twitter",
        BackgroundColor = "#55ACEE",
        Url = "https://twitter.com/")]
    Twitter = 8,

    [ServiceInfo(
        Name = "instagram",
        Title = "Instagram",
        BackgroundColor = "#AD8466",
        Url = "https://instagram.com/")]
    Instagram = 9,

    [ServiceInfo(
        Name = "flickr",
        Title = "Flickr",
        BackgroundColor = "#0063DC",
        Url = "https://www.flickr.com/photos/")]
    Flickr = 10,

    [ServiceInfo(
        Name = "ns",
        Title = "NS",
        BackgroundColor = "#E30B20",
        ValidationRegex = @"^SW-[0-9]{4}-[0-9]{4}-[0-9]{4}$")]
    NS = 11,
}
