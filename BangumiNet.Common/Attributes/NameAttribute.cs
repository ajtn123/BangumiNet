namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class NameCnAttribute(string value) : Attribute
{
    public string NameCn { get; init; } = value;
}

[AttributeUsage(AttributeTargets.Field)]
public class NameEnAttribute(string value) : Attribute
{
    public string NameEn { get; init; } = value;
}

[AttributeUsage(AttributeTargets.Field)]
public class NameJpAttribute(string value) : Attribute
{
    public string NameJp { get; init; } = value;
}
