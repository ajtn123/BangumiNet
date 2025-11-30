namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class PlatformInfoAttribute : Attribute
{
    public required string Name { get; init; }
    public required string NameCn { get; init; }
    public required string Alias { get; init; }
    public required int Order { get; init; }
    public string[]? SearchKeywords { get; init; }
    public string[]? SortKeys { get; init; }
    public string? WikiTemplate { get; init; }
    public bool IsHeaderEnabled { get; init; }
}

[AttributeUsage(AttributeTargets.Field)]
public class SpecificTypeAttribute(Type type) : Attribute
{
    public Type SpecificType { get; init; } = type;
}

[AttributeUsage(AttributeTargets.Enum)]
public class ParentTypeAttribute<T>(T type) : Attribute where T : Enum
{
    public T ParentType { get; init; } = type;
}
