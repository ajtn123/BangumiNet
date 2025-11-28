namespace BangumiNet.Api.ExtraEnums;

public enum RelatedItemType
{
    Character,
    Collector,
    Comment,
    Episode,
    Index,
    Recommendation,
    Subject,
    Review,
    Person,
    Position,
    Topic,
    Photo,

    /// <summary>角色出演的作品</summary>
    CharacterCast,
    /// <summary>人物出演的角色</summary>
    PersonCast,
    /// <summary>人物参与制作的作品</summary>
    PersonWork,
}

public static partial class EnumExtensions
{
    public static string? ToStringSC(this RelatedItemType type) => type switch
    {
        RelatedItemType.Character => "角色",
        RelatedItemType.Collector => "收藏用户",
        RelatedItemType.Comment => "吐槽箱",
        RelatedItemType.Episode => "章节",
        RelatedItemType.Index => "关联目录",
        RelatedItemType.Recommendation => "推荐",
        RelatedItemType.Subject => "关联条目",
        RelatedItemType.Review => "评论",
        RelatedItemType.Person => "制作人员",
        RelatedItemType.Position => "制作人员职位",
        RelatedItemType.Topic => "讨论版",
        RelatedItemType.Photo => "图片",

        RelatedItemType.CharacterCast => "出演作品",
        RelatedItemType.PersonCast => "出演角色",
        RelatedItemType.PersonWork => "参与作品",

        _ => throw new NotImplementedException(),
    };
}
