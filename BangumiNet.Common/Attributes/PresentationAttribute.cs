namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
[Keywords("颜色检查", "茶水", "其他电视台", "顾问", "仕上")]
public class FoldedAttribute : Attribute;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Class)]
public class KeywordsAttribute(params string[] keywords) : Attribute
{
    public string[] Keywords { get; init; } = keywords;
}
