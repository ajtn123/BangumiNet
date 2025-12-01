using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Extras;

/// <summary>
/// 章节类型
/// </summary>
public enum EpisodeType
{
    /// <summary>
    /// 本篇
    /// </summary>
    [NameCn("本篇")]
    Mainline = 0,

    /// <summary>
    /// SP/特别篇
    /// </summary>
    [NameCn("SP")]
    Special = 1,

    /// <summary>
    /// OP
    /// </summary>
    [NameCn("OP")]
    Opening = 2,

    /// <summary>
    /// ED
    /// </summary>
    [NameCn("ED")]
    Ending = 3,

    /// <summary>
    /// PV/预告 CM/宣传 AD/广告
    /// </summary>
    [NameCn("PV/CM/AD")]
    Advertisement = 4,

    /// <summary>
    /// MAD
    /// </summary>
    [NameCn("MAD")]
    Mad = 5,

    /// <summary>
    /// 其他
    /// </summary>
    [NameCn("其他")]
    Other = 6,
}
