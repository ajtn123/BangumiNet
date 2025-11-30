namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ViceVersaAttribute<T>(T value) : Attribute where T : Enum
{
    public T Value { get; init; } = value;
}

[AttributeUsage(AttributeTargets.Field)]
public class SkipViceVersaAttribute : Attribute;
