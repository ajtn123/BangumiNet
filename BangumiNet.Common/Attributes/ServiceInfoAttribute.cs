namespace BangumiNet.Common.Attributes;

[AttributeUsage(AttributeTargets.Field)]
public class ServiceInfoAttribute : Attribute
{
    public required string Name { get; init; }
    public required string Title { get; init; }
    public required string BackgroundColor { get; init; }
    public string? Url { get; init; }
    public string? ValidationRegex { get; init; }
}
