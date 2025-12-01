using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Extras;

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType
{
    /// <summary>
    /// 角色
    /// </summary>
    [NameCn("角色")]
    Individual = 1,

    /// <summary>
    /// 机体
    /// </summary>
    [NameCn("机体")]
    Bot = 2,

    /// <summary>
    /// 舰船
    /// </summary>
    [NameCn("舰船")]
    Ship = 3,

    /// <summary>
    /// 组织
    /// </summary>
    [NameCn("组织")]
    Organization = 4,
}
