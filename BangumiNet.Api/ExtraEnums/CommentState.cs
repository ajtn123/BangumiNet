namespace BangumiNet.Api.ExtraEnums;

//https://github.com/bangumi/server-private/blob/master/lib/topic/type.ts
public enum CommentState
{
    /// <summary>正常</summary>
    Normal = 0,
    /// <summary>管理员关闭主题</summary>
    AdminCloseTopic = 1,
    /// <summary>重开主题</summary>
    AdminReopen = 2,
    /// <summary>置顶</summary>
    AdminPin = 3,
    /// <summary>合并</summary>
    AdminMerge = 4,
    /// <summary>管理员下沉</summary>
    AdminSilentTopic = 5,
    /// <summary>自行删除</summary>
    UserDelete = 6,
    /// <summary>管理员删除</summary>
    AdminDelete = 7,
    /// <summary>折叠</summary>
    AdminOffTopic = 8,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this CommentState state)
        => state switch
        {
            CommentState.Normal => "正常",
            CommentState.AdminCloseTopic => "关闭",
            CommentState.AdminReopen => "重开",
            CommentState.AdminPin => "置顶",
            CommentState.AdminMerge => "合并",
            CommentState.AdminSilentTopic => "下沉",
            CommentState.UserDelete => "已删除",
            CommentState.AdminDelete => "管理员移除",
            CommentState.AdminOffTopic => "折叠",
            _ => throw new NotImplementedException(),
        };
}
