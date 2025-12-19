using BangumiNet.Api.V0.V0.Search.Subjects;
using BangumiNet.Common.Attributes;
using System.Runtime.Serialization;

namespace BangumiNet.Api.ExtraEnums;

public enum SubjectBrowserSort
{
    [EnumMember(Value = "date")]
    Date,
    [EnumMember(Value = "rank")]
    Rank,
}
public static partial class EnumExtensions
{
    public static string ToStringSC(this SubjectsPostRequestBody_sort type)
        => type switch
        {
            SubjectsPostRequestBody_sort.Match => "相关",
            SubjectsPostRequestBody_sort.Heat => "热度",
            SubjectsPostRequestBody_sort.Rank => "排名",
            SubjectsPostRequestBody_sort.Score => "评分",
            _ => throw new NotImplementedException(),
        };
    public static string ToStringSC(this SubjectBrowserSort type)
        => type switch
        {
            SubjectBrowserSort.Date => "日期",
            SubjectBrowserSort.Rank => "排名",
            _ => throw new NotImplementedException(),
        };
    public static string GetValue(this SubjectBrowserSort type)
        => AttributeHelpers.GetAttribute<EnumMemberAttribute>(type)!.Value!;
}
