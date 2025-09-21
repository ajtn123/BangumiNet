namespace BangumiNet.Api.ExtraEnums;

/// <summary>
/// 条目类型，没有 <c>5</c>
/// </summary>
public enum SubjectType
{
    /// <summary>
    /// 书籍
    /// </summary>
    Book = 1,
    /// <summary>
    /// 动画
    /// </summary>
    Anime = 2,
    /// <summary>
    /// 音乐
    /// </summary>
    Music = 3,
    /// <summary>
    /// 游戏
    /// </summary>
    Game = 4,
    /// <summary>
    /// 三次元
    /// </summary>
    RealLife = 6,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this SubjectType type)
        => type switch
        {
            SubjectType.Book => "书籍",
            SubjectType.Anime => "动画",
            SubjectType.Music => "音乐",
            SubjectType.Game => "游戏",
            SubjectType.RealLife => "三次元",
            _ => throw new NotImplementedException(),
        };
}
