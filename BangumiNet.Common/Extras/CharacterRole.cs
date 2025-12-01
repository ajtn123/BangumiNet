using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Extras;

// https://github.com/bangumi/server/blob/master/web/res/character.go

/// <summary>
/// 角色在条目内的类型
/// </summary>
public enum CharacterRole
{
    /// <summary>
    /// 主角
    /// </summary>
    [NameCn("主角")]
    Main = 1,

    /// <summary>
    /// 配角
    /// </summary>
    [NameCn("配角")]
    Supporting = 2,

    /// <summary>
    /// 客串
    /// </summary>
    [NameCn("客串")]
    Cameo = 3,

    /// <summary>
    /// 闲角
    /// </summary>
    [NameCn("闲角")]
    Minor = 4,

    /// <summary>
    /// 旁白
    /// </summary>
    [NameCn("旁白")]
    Narrator = 5,

    /// <summary>
    /// 声库
    /// </summary>
    [NameCn("声库")]
    VoiceBank = 6,
}
