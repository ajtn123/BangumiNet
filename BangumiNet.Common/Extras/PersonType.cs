using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Extras;

/// <summary>
/// 人物类型
/// </summary>
public enum PersonType
{
    /// <summary>
    /// 个人
    /// </summary>
    [NameCn("个人")]
    Individual = 1,

    /// <summary>
    /// 公司
    /// </summary>
    [NameCn("公司")]
    Company = 2,

    /// <summary>
    /// 组合
    /// </summary>
    [NameCn("组合")]
    Group = 3
}
