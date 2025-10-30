namespace BangumiNet.Api.ExtraEnums;

public enum TimelineCategory
{
    Routine = 1,
    WikiOperation = 2,
    Collection = 3,
    Progress = 4,
    Status = 5,
    Blog = 6,
    Index = 7,
    Character = 8,
    doujin = 9,
}

public static partial class EnumExtensions
{
    public static string ToStringSC(this TimelineCategory category) => category switch
    {
        TimelineCategory.Routine => "日常行为",
        TimelineCategory.WikiOperation => "维基操作",
        TimelineCategory.Collection => "收藏条目",
        TimelineCategory.Progress => "收视进度",
        TimelineCategory.Status => "状态",
        TimelineCategory.Blog => "日志",
        TimelineCategory.Index => "目录",
        TimelineCategory.Character => "人物",
        TimelineCategory.doujin => "天窗",
        _ => throw new NotImplementedException(),
    };
}