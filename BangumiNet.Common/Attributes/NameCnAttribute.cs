namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class NameCnAttribute(string cn) : Attribute
{
    public string NameCn { get; init; } = cn;
}
