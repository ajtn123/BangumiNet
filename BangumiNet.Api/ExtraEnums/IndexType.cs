namespace BangumiNet.Api.ExtraEnums;
// https://github.com/bangumi/server-private/blob/master/lib/index/types.ts
// https://github.com/bangumi/server-private/blob/master/lib/types/common.ts
public enum IndexType
{
    User = 0,
    Public = 1,
    Award = 2,
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
