namespace BangumiNet.Api.ExtraEnums;

public enum SubjectBrowserSort
{
    Date,
    Rank
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this SubjectBrowserSort type)
        => type switch
        {
            SubjectBrowserSort.Date => "日期",
            SubjectBrowserSort.Rank => "排名",
            _ => throw new NotImplementedException(),
        };
}
