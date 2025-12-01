using BangumiNet.Common.Attributes;

namespace BangumiNet.Common;

public enum SubjectType
{
    [PlatformInfo(
        Name = "Book",
        NameCn = "书籍",
        Alias = "Book",
        Order = 1,
        SortKeys = ["发售日"],
        WikiTemplate = WikiTemplates.Book)]
    [SpecificType(typeof(BookType))]
    Book = 1,

    [PlatformInfo(
        Name = "Anime",
        NameCn = "动画",
        Alias = "Anime",
        Order = 2,
        SortKeys = ["放送开始", "发售日", "发售日期", "上映年度", "上映日"],
        WikiTemplate = WikiTemplates.Anime)]
    [SpecificType(typeof(AnimeType))]
    Anime = 2,

    [PlatformInfo(
        Name = "Music",
        NameCn = "音乐",
        Alias = "Album",
        Order = 3,
        SortKeys = ["发售日期"],
        WikiTemplate = WikiTemplates.Album)]
    [SpecificType(typeof(MusicType))]
    Music = 3,

    [PlatformInfo(
        Name = "Game",
        NameCn = "游戏",
        Alias = "Game",
        Order = 4,
        SortKeys = ["发行日期"],
        WikiTemplate = WikiTemplates.Game)]
    [SpecificType(typeof(GameType))]
    Game = 4,

    [PlatformInfo(
        Name = "Real",
        NameCn = "三次元",
        Alias = "TV",
        Order = 5,
        SortKeys = ["上映日", "开始"],
        WikiTemplate = WikiTemplates.TV)]
    [SpecificType(typeof(RealType))]
    Real = 6,
}

[ParentType<SubjectType>(SubjectType.Book)]
public enum BookType
{
    [PlatformInfo(
        Name = "Comic",
        NameCn = "漫画",
        Alias = "comic",
        WikiTemplate = WikiTemplates.Manga,
        IsHeaderEnabled = true,
        Order = 0)]
    Comic = 1001,

    [PlatformInfo(
        Name = "Novel",
        NameCn = "小说",
        Alias = "novel",
        WikiTemplate = WikiTemplates.Novel,
        IsHeaderEnabled = true,
        Order = 1)]
    Novel = 1002,

    [PlatformInfo(
        Name = "Illustration",
        NameCn = "画集",
        Alias = "illustration",
        WikiTemplate = WikiTemplates.Book,
        IsHeaderEnabled = true,
        Order = 2)]
    Illustration = 1003,

    [PlatformInfo(
        Name = "Picture",
        NameCn = "绘本",
        Alias = "picture",
        WikiTemplate = WikiTemplates.Book,
        IsHeaderEnabled = true,
        Order = 3)]
    PictureBook = 1004,

    [PlatformInfo(
        Name = "Official",
        NameCn = "公式书",
        Alias = "official",
        WikiTemplate = WikiTemplates.Book,
        IsHeaderEnabled = true,
        Order = 4)]
    Official = 1006,

    [PlatformInfo(
        Name = "Photo",
        NameCn = "写真",
        Alias = "photo",
        WikiTemplate = WikiTemplates.PhotoBook,
        IsHeaderEnabled = true,
        Order = 5)]
    PhotoBook = 1005,

    [PlatformInfo(
        Name = "other",
        NameCn = "其他",
        Alias = "misc",
        WikiTemplate = WikiTemplates.Book,
        Order = 6)]
    None = 0,
}

public enum BookSeriesType
{
    [PlatformInfo(
        Name = "offprint",
        NameCn = "单行本",
        Alias = "offprint",
        Order = 1)]
    Offprint = 0,

    [PlatformInfo(
        Name = "series",
        NameCn = "系列",
        Alias = "series",
        Order = 0)]
    Series = 1,
}

[ParentType<SubjectType>(SubjectType.Anime)]
public enum AnimeType
{

    [PlatformInfo(
        Name = "TV",
        NameCn = "TV",
        Alias = "tv",
        WikiTemplate = WikiTemplates.TVAnime,
        IsHeaderEnabled = true,
        Order = 0,
        SortKeys = [])]
    TV = 1,

    [PlatformInfo(
        Name = "web",
        NameCn = "WEB",
        Alias = "web",
        WikiTemplate = WikiTemplates.TVAnime,
        IsHeaderEnabled = true,
        Order = 1,
        SortKeys = [])]
    Web = 5,

    [PlatformInfo(
        Name = "OVA",
        NameCn = "OVA",
        Alias = "ova",
        WikiTemplate = WikiTemplates.OVA,
        IsHeaderEnabled = true,
        Order = 2,
        SortKeys = [])]
    OVA = 2,

    [PlatformInfo(
        Name = "movie",
        NameCn = "剧场版",
        Alias = "movie",
        WikiTemplate = WikiTemplates.Movie,
        IsHeaderEnabled = true,
        Order = 3,
        SortKeys = [])]
    Movie = 3,

    [PlatformInfo(
        Name = "anime_comic",
        NameCn = "动态漫画",
        Alias = "anime_comic",
        WikiTemplate = WikiTemplates.TVAnime,
        IsHeaderEnabled = true,
        Order = 4)]
    AnimeComic = 2006,

    //ShortFilm = 4,

    [PlatformInfo(
        Name = "other",
        NameCn = "其他",
        Alias = "misc",
        WikiTemplate = WikiTemplates.Anime,
        Order = 5,
        SortKeys = [])]
    None = 0,
}

[ParentType<SubjectType>(SubjectType.Real)]
public enum RealType
{
    [PlatformInfo(
        Name = "jp",
        NameCn = "日剧",
        Alias = "jp",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 0)]
    JP = 1,

    [PlatformInfo(
        Name = "en",
        NameCn = "欧美剧",
        Alias = "en",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 1)]
    EN = 2,

    [PlatformInfo(
        Name = "cn",
        NameCn = "华语剧",
        Alias = "cn",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 2)]
    CN = 3,

    [PlatformInfo(
        Name = "tv",
        NameCn = "电视剧",
        Alias = "tv",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 3)]
    TV = 6001,

    [PlatformInfo(
        Name = "movie",
        NameCn = "电影",
        Alias = "movie",
        WikiTemplate = WikiTemplates.RealMovie,
        IsHeaderEnabled = true,
        Order = 4)]
    Movie = 6002,

    [PlatformInfo(
        Name = "live",
        NameCn = "演出",
        Alias = "live",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 5)]
    Live = 6003,

    [PlatformInfo(
        Name = "show",
        NameCn = "综艺",
        Alias = "show",
        WikiTemplate = WikiTemplates.TV,
        IsHeaderEnabled = true,
        Order = 6)]
    Show = 6004,

    [PlatformInfo(
        Name = "other",
        NameCn = "其他",
        Alias = "misc",
        WikiTemplate = WikiTemplates.TV,
        Order = 7)]
    None = 0,
}

[ParentType<SubjectType>(SubjectType.Music)]
public enum MusicType
{
    Album = 3001,
    Drama = 3002,
    Audio = 3003,
    Radio = 3004,
}

[ParentType<SubjectType>(SubjectType.Game)]
public enum GameType
{
    [PlatformInfo(
        Name = "games",
        NameCn = "游戏",
        Alias = "games",
        IsHeaderEnabled = true,
        Order = 0)]
    Games = 4001,

    [PlatformInfo(
        Name = "dlc",
        NameCn = "扩展包",
        Alias = "dlc",
        IsHeaderEnabled = true,
        Order = 1)]
    DLC = 4003,

    //[PlatformInfo(
    //    Name = "demo",
    //    NameCn = "试玩版",
    //    Alias = "demo",
    //    Order = )]
    //Demo = 4004,

    [PlatformInfo(
        Name = "software",
        NameCn = "软件",
        Alias = "software",
        IsHeaderEnabled = true,
        Order = 2)]
    Software = 4002,

    [PlatformInfo(
        Name = "tabletop",
        NameCn = "桌游",
        Alias = "tabletop",
        IsHeaderEnabled = true,
        Order = 3)]
    Table = 4005,

    [PlatformInfo(
        Name = "other",
        NameCn = "其他",
        Alias = "misc",
        Order = 4)]
    None = 0,
}

public enum GamePlatform
{
    [PlatformInfo(
        Name = "PC",
        NameCn = "PC",
        Alias = "pc",
        SearchKeywords = ["pc", "windows"],
        Order = 0)]
    PC = 4,

    [PlatformInfo(
        Name = "Mac OS",
        NameCn = "Mac OS",
        Alias = "mac",
        SearchKeywords = ["mac"],
        Order = 1)]
    MacOS = 33,

    [PlatformInfo(
        Name = "PS5",
        NameCn = "PS5",
        Alias = "ps5",
        SearchKeywords = ["PS5"],
        Order = 2)]
    PS5 = 38,

    [PlatformInfo(
        Name = "Xbox Series X/S",
        NameCn = "Xbox Series X/S",
        Alias = "xbox_series_xs",
        SearchKeywords = ["XSX", "XSS", "Xbox Series X", "Xbox Series S"],
        Order = 3)]
    XboxSeriesXS = 39,

    [PlatformInfo(
        Name = "PS4",
        NameCn = "PS4",
        Alias = "ps4",
        SearchKeywords = ["PS4"],
        Order = 4)]
    PS4 = 34,

    [PlatformInfo(
        Name = "Xbox One",
        NameCn = "Xbox One",
        Alias = "xbox_one",
        SearchKeywords = ["Xbox One"],
        Order = 5)]
    XboxOne = 35,

    [PlatformInfo(
        Name = "Nintendo Switch",
        NameCn = "Nintendo Switch",
        Alias = "ns",
        SearchKeywords = ["Switch", "NS"],
        Order = 6)]
    NintendoSwitch = 37,

    [PlatformInfo(
        Name = "Wii U",
        NameCn = "Wii U",
        Alias = "wii_u",
        SearchKeywords = ["Wii U", "WiiU"],
        Order = 7)]
    WiiU = 36,

    [PlatformInfo(
        Name = "PS3",
        NameCn = "PS3",
        Alias = "ps3",
        SearchKeywords = ["PS3", "PlayStation 3"],
        Order = 8)]
    PS3 = 8,

    [PlatformInfo(
        Name = "Xbox360",
        NameCn = "Xbox360",
        Alias = "xbox360",
        SearchKeywords = ["xbox360"],
        Order = 9)]
    Xbox360 = 9,

    [PlatformInfo(
        Name = "Wii",
        NameCn = "Wii",
        Alias = "wii",
        SearchKeywords = ["Wii"],
        Order = 10)]
    Wii = 10,

    [PlatformInfo(
        Name = "PSVita",
        NameCn = "PS Vita",
        Alias = "psv",
        SearchKeywords = ["psv", "vita"],
        Order = 11)]
    PSVita = 30,

    [PlatformInfo(
        Name = "3DS",
        NameCn = "3DS",
        Alias = "3ds",
        SearchKeywords = ["3ds"],
        Order = 12)]
    ThreeDS = 31,

    [PlatformInfo(
        Name = "iOS",
        NameCn = "iOS",
        Alias = "iphone",
        SearchKeywords = ["iphone", "ipad", "ios"],
        Order = 13)]
    iOS = 11,

    [PlatformInfo(
        Name = "Android",
        NameCn = "Android",
        Alias = "android",
        SearchKeywords = ["android"],
        Order = 14)]
    Android = 32,

    [PlatformInfo(
        Name = "ARC",
        NameCn = "街机",
        Alias = "arc",
        SearchKeywords = ["ARC", "街机"],
        Order = 15)]
    ARC = 12,

    [PlatformInfo(
        Name = "NDS",
        NameCn = "NDS",
        Alias = "nds",
        SearchKeywords = ["nds"],
        Order = 16)]
    NDS = 5,

    [PlatformInfo(
        Name = "PSP",
        NameCn = "PSP",
        Alias = "psp",
        SearchKeywords = ["psp"],
        Order = 17)]
    PSP = 6,

    //[PlatformInfo(
    //    Type = "Mobile",
    //    TypeCn = "手机",
    //    Alias = "mobile",
    //    SearchKeywords = ["mobile"],
    //    Order = )]
    // Mobile = 13,

    [PlatformInfo(
        Name = "PS2",
        NameCn = "PS2",
        Alias = "ps2",
        SearchKeywords = ["PS2"],
        Order = 18)]
    PS2 = 7,

    [PlatformInfo(
        Name = "XBOX",
        NameCn = "XBOX",
        Alias = "xbox",
        SearchKeywords = ["XBOX"],
        Order = 19)]
    XBOX = 15,

    [PlatformInfo(
        Name = "GameCube",
        NameCn = "GameCube",
        Alias = "gamecube",
        SearchKeywords = ["GameCube", "ngc"],
        Order = 20)]
    GameCube = 17,

    [PlatformInfo(
        Name = "Dreamcast",
        NameCn = "Dreamcast",
        Alias = "dreamcast",
        SearchKeywords = ["dc"],
        Order = 21)]
    Dreamcast = 27,

    [PlatformInfo(
        Name = "Nintendo 64",
        NameCn = "Nintendo 64",
        Alias = "n64",
        SearchKeywords = ["n64"],
        Order = 22)]
    Nintendo64 = 21,

    [PlatformInfo(
        Name = "PlayStation",
        NameCn = "PlayStation",
        Alias = "ps",
        SearchKeywords = ["ps"],
        Order = 23)]
    PlayStation = 28,

    [PlatformInfo(
        Name = "SFC",
        NameCn = "SFC",
        Alias = "sfc",
        SearchKeywords = ["SFC"],
        Order = 24)]
    SFC = 19,

    [PlatformInfo(
        Name = "FC",
        NameCn = "FC",
        Alias = "fc",
        SearchKeywords = ["FC"],
        Order = 25)]
    FC = 20,

    [PlatformInfo(
        Name = "WonderSwan",
        NameCn = "WonderSwan",
        Alias = "ws",
        SearchKeywords = ["ws"],
        Order = 26)]
    WonderSwan = 29,

    [PlatformInfo(
        Name = "WonderSwan Color",
        NameCn = "WonderSwan Color",
        Alias = "wsc",
        SearchKeywords = ["wsc"],
        Order = 27)]
    WonderSwanColor = 26,

    [PlatformInfo(
        Name = "NEOGEO Pocket Color",
        NameCn = "NEOGEO Pocket Color",
        Alias = "ngp",
        SearchKeywords = ["ngp"],
        Order = 28)]
    NEOGEOPocketColor = 18,

    [PlatformInfo(
        Name = "GBA",
        NameCn = "GBA",
        Alias = "GBA",
        SearchKeywords = ["GBA"],
        Order = 29)]
    GBA = 22,

    [PlatformInfo(
        Name = "GB",
        NameCn = "GB",
        Alias = "GB",
        SearchKeywords = ["GB"],
        Order = 30)]
    GB = 23,

    [PlatformInfo(
        Name = "Virtual Boy",
        NameCn = "Virtual Boy",
        Alias = "vb",
        SearchKeywords = ["Virtual Boy"],
        Order = 31)]
    VirtualBoy = 25,
}
