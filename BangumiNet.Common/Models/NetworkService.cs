namespace BangumiNet.Common.Models;

public readonly record struct NetworkService
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Title { get; init; }
    public string? Url { get; init; }
    public string BackgroundColor { get; init; }
    public string? ValidationRegex { get; init; }
}
