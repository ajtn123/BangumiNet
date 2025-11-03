using static BangumiNet.Api.ExtraEnums.SubjectCategory;

namespace BangumiNet.Api.ExtraEnums;

public static class SubjectCategory
{
    public enum Book
    {
        /// <summary>其他</summary>
        Other = 0,
        /// <summary>漫画</summary>
        Comic = 1001,
        /// <summary>小说</summary>
        Novel = 1002,
        /// <summary>画集</summary>
        Illustration = 1003,
    }
    public enum Anime
    {
        /// <summary>其他</summary>
        Other = 0,
        /// <summary>TV</summary>
        TV = 1,
        /// <summary>OVA</summary>
        OVA = 2,
        /// <summary>Movie</summary>
        Movie = 3,
        /// <summary>WEB</summary>
        WEB = 5,
    }
    public enum Game
    {
        /// <summary>其他</summary>
        Other = 0,
        /// <summary>游戏</summary>
        Games = 4001,
        /// <summary>软件</summary>
        Software = 4002,
        /// <summary>扩展包</summary>
        DLC = 4003,
        /// <summary>桌游</summary>
        Tabletop = 4005,
    }
    public enum Real
    {
        /// <summary>其他</summary>
        Other = 0,
        /// <summary>日剧</summary>
        JP = 1,
        /// <summary>欧美剧</summary>
        EN = 2,
        /// <summary>华语剧</summary>
        CN = 3,
        /// <summary>电视剧</summary>
        TV = 6001,
        /// <summary>电影</summary>
        Movie = 6002,
        /// <summary>演出</summary>
        Live = 6003,
        /// <summary>综艺</summary>
        Show = 6004,
    }
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this Anime type)
        => type switch
        {
            Anime.Other => "其他",
            Anime.TV => "TV",
            Anime.OVA => "OVA",
            Anime.Movie => "Movie",
            Anime.WEB => "WEB",
            _ => throw new NotImplementedException(),
        };
    public static string ToStringSC(this Book type)
        => type switch
        {
            Book.Other => "其他",
            Book.Comic => "漫画",
            Book.Novel => "小说",
            Book.Illustration => "插画",
            _ => throw new NotImplementedException(),
        };
    public static string ToStringSC(this Game type)
        => type switch
        {
            Game.Other => "其他",
            Game.Games => "游戏",
            Game.Software => "软件",
            Game.DLC => "扩展包",
            Game.Tabletop => "桌游",
            _ => throw new NotImplementedException(),
        };
    public static string ToStringSC(this Real type)
        => type switch
        {
            Real.Other => "其他",
            Real.JP => "日剧",
            Real.EN => "欧美剧",
            Real.CN => "华语剧",
            Real.TV => "电视剧",
            Real.Movie => "电影",
            Real.Live => "演出",
            Real.Show => "综艺",
            _ => throw new NotImplementedException(),
        };
    public static Type? GetCategories(this SubjectType type)
    => type switch
    {
        SubjectType.Book => typeof(Book),
        SubjectType.Anime => typeof(Anime),
        SubjectType.Music => null,
        SubjectType.Game => typeof(Game),
        SubjectType.Real => typeof(Real),
        _ => throw new NotImplementedException(),
    };
}
