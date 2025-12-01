namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class SourceInfoAttribute : Attribute
{
    public required string Name { get; init; }
    public string? Url { get; init; }
    public string? AppId { get; init; }
}
