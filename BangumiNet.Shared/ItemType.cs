using BangumiNet.Common.Attributes;

namespace BangumiNet.Shared;

public enum ItemType
{
    [NameCn("未知")]
    Unknown,

    [NameCn("条目")]
    Subject,

    [NameCn("章节")]
    Episode,

    [NameCn("角色")]
    Character,

    [NameCn("人物")]
    Person,

    [NameCn("用户")]
    User,

    [NameCn("话题")]
    Topic,

    [NameCn("小组")]
    Group,

    [NameCn("时间线")]
    Timeline,

    [NameCn("修订")]
    Revision,

    [NameCn("日志")]
    Blog,

    [NameCn("图片")]
    Photo,

    [NameCn("目录")]
    Index,

    [NameCn("封面")]
    Cover,
}
