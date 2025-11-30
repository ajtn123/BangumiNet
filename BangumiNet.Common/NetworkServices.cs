using BangumiNet.Common.Models;

namespace BangumiNet.Common;

public static partial class Config
{
    public static NetworkService[] NetworkServices { get; } = [
        new NetworkService { Id = 1, Name = "psn", Title = "PSN", BackgroundColor = "#004AB0", Url = "https://www.playstation.com/en-us/my/public-trophies/?onlineId=" },
        new NetworkService { Id = 2, Name = "live", Title = "Xbox Live", BackgroundColor = "#107C11", Url = "https://account.xbox.com/en-US/Profile?gamerTag=", ValidationRegex = @"^[A-Za-z0-90-9]*( )?_?\-?\.?[A-Za-z0-9]*" },
        new NetworkService { Id = 3, Name = "fc", Title = "FriendCode", BackgroundColor = "#EE1C23", ValidationRegex = @"^[0-9]{4}-[0-9]{4}-[0-9]{4}$" },
        new NetworkService { Id = 4, Name = "steam", Title = "Steam", BackgroundColor = "#171A21", Url = "https://steamcommunity.com/id/" },
        new NetworkService { Id = 5, Name = "battletag", Title = "BattleTag", BackgroundColor = "#007CAE", ValidationRegex = @"^[A-Za-z][A-Za-z0-9]*#[0-9]*$" },
        new NetworkService { Id = 6, Name = "pixiv", Title = "Pixiv", BackgroundColor = "#0097DC", Url = "https://www.pixiv.net/users/", ValidationRegex = @"^[0-9]*$" },
        new NetworkService { Id = 7, Name = "github", Title = "GitHub", BackgroundColor = "#333", Url = "https://github.com/" },
        new NetworkService { Id = 8, Name = "twitter", Title = "Twitter", BackgroundColor = "#55ACEE", Url = "https://twitter.com/" },
        new NetworkService { Id = 9, Name = "instagram", Title = "Instagram", BackgroundColor = "#AD8466", Url = "https://instagram.com/" },
        new NetworkService { Id = 10, Name = "flickr", Title = "Flickr", BackgroundColor = "#0063DC", Url = "https://www.flickr.com/photos/" },
        new NetworkService { Id = 11, Name = "ns", Title = "NS", BackgroundColor = "#E30B20", ValidationRegex = @"^SW-[0-9]{4}-[0-9]{4}-[0-9]{4}$" },
    ];
}
