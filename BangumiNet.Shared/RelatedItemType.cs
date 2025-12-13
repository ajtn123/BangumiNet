using BangumiNet.Common.Attributes;

namespace BangumiNet.Shared;

public enum RelatedItemType
{
    [NameCn("角色")]
    Character,

    [NameCn("收藏用户")]
    Collector,

    [NameCn("吐槽箱")]
    Comment,

    [NameCn("章节")]
    Episode,

    [NameCn("关联目录")]
    Index,

    [NameCn("推荐")]
    Recommendation,

    [NameCn("关联条目")]
    Subject,

    [NameCn("评论")]
    Review,

    [NameCn("制作人员")]
    Person,

    [NameCn("制作人员职位")]
    Position,

    [NameCn("讨论版")]
    Topic,

    [NameCn("图片")]
    Photo,

    /// <summary>角色出演的作品</summary>
    [NameCn("出演作品")]
    CharacterCast,

    /// <summary>人物出演的角色</summary>
    [NameCn("出演角色")]
    PersonCast,

    /// <summary>人物参与制作的作品</summary>
    [NameCn("参与作品")]
    PersonWork,
}
