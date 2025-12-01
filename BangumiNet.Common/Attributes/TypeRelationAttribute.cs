namespace BangumiNet.Common.Attributes;

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

[AttributeUsage(AttributeTargets.Field)]
public class CategoriesAttribute<T>(params T[] type) : Attribute where T : Enum
{
    public T[] Categories { get; init; } = type;
}
