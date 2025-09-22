namespace BangumiNet.Api.ExtraEnums;

/// <summary>章节类型</summary>
public enum EpisodeType
{
    /// <summary>本篇</summary>
    Mainline = 0,
    /// <summary>SP/特别篇</summary>
    Special = 1,
    /// <summary>OP</summary>
    Opening = 2,
    /// <summary>ED</summary>
    Ending = 3,
    /// <summary>PV/预告 CM/宣传 AD/广告</summary>
    Advertisement = 4,
    /// <summary>MAD</summary>
    Mad = 5,
    /// <summary>其他</summary>
    Other = 6,
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this EpisodeType type)
        => type switch
        {
            EpisodeType.Mainline => "本篇",
            EpisodeType.Special => "SP",
            EpisodeType.Opening => "OP",
            EpisodeType.Ending => "ED",
            EpisodeType.Advertisement => "PV/CM/AD",
            EpisodeType.Mad => "MAD",
            EpisodeType.Other => "其他",
            _ => throw new NotImplementedException(),
        };
}
