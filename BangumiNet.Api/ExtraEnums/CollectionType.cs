namespace BangumiNet.Api.ExtraEnums;

/// <summary>
/// 收藏状态
/// </summary>
public enum CollectionType
{
    /// <summary>
    /// 想看
    /// </summary>
    Wish = 1,
    /// <summary>
    /// 看过
    /// </summary>
    Done = 2,
    /// <summary>
    /// 在看
    /// </summary>
    Doing = 3,
    /// <summary>
    /// 搁置
    /// </summary>
    OnHold = 4,
    /// <summary>
    /// 抛弃
    /// </summary>
    Dropped = 5,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this CollectionType type)
        => type switch
        {
            CollectionType.Wish => "想看",
            CollectionType.Done => "看过",
            CollectionType.Doing => "在看",
            CollectionType.OnHold => "搁置",
            CollectionType.Dropped => "抛弃",
            _ => throw new NotImplementedException(),
        };
}
