namespace BangumiNet.Api.ExtraEnums;

/// <summary>小组成员角色</summary>
public enum GroupRole
{
    ///<summary>访客</summary>
    Guest = -2,
    ///<summary>游客</summary>
    Visitor = -1,
    ///<summary>小组成员</summary>
    Member = 0,
    ///<summary>小组长</summary>
    Leader = 1,
    ///<summary>小组管理员</summary>
    Administrator = 2,
    /// <summary>禁言成员</summary>
    Banned = 3,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this GroupRole role) => role switch
    {
        GroupRole.Guest => "访客",
        GroupRole.Visitor => "游客",
        GroupRole.Member => "小组成员",
        GroupRole.Leader => "小组长",
        GroupRole.Administrator => "小组管理员",
        GroupRole.Banned => "禁言成员",
        _ => throw new NotImplementedException(),
    };
}
