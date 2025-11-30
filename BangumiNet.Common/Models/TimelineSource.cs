namespace BangumiNet.Common.Models;

public readonly record struct TimelineSource
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string? Url { get; init; }
    public string? AppID { get; init; }
}
