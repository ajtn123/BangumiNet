namespace BangumiNet.Api.ExtraEnums;

public enum UserGroup
{
    /// <summary>管理员</summary>
    Administrator = 1,
    /// <summary>Bangumi 管理猿</summary>
    BangumiAdministrator = 2,
    /// <summary>天窗管理猿</summary>
    DoujinAdministrator = 3,
    /// <summary>禁言用户</summary>
    MutedUser = 4,
    /// <summary>禁止访问用户</summary>
    BannedUser = 5,
    /// <summary>人物管理猿</summary>
    PersonAdministrator = 8,
    /// <summary>维基条目管理猿</summary>
    WikiAdministrator = 9,
    /// <summary>用户（最常见）</summary>
    User = 10,
    /// <summary>维基人</summary>
    Wikipedian = 11,
}
