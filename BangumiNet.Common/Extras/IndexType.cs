using BangumiNet.Common.Attributes;

namespace BangumiNet.Common.Extras;

// https://github.com/bangumi/server-private/blob/master/lib/index/types.ts
// https://github.com/bangumi/server-private/blob/master/lib/types/common.ts

/// <summary>
/// 目录类型
/// </summary>
public enum IndexType
{
    /// <summary>
    /// 用户
    /// </summary>
    [NameCn("用户")]
    User = 0,

    /// <summary>
    /// 公共
    /// </summary>
    [NameCn("公共")]
    Public = 1,

    /// <summary>
    /// The Bangumi Awards
    /// </summary>
    [NameCn("TBA")]
    TBA = 2,
}

public enum IndexPrivacy
{
    Normal = 0,
    Ban = 1,
    Private = 2,
}

public enum IndexRelatedCategory
{
    Subject = 0,
    Character = 1,
    Person = 2,
    Episode = 3,
    Blog = 4,
    GroupTopic = 5,
    SubjectTopic = 6,
}
