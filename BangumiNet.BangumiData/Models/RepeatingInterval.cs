namespace BangumiNet.BangumiData.Models;

public readonly record struct RepeatingInterval
{
    public DateTimeOffset Start { get; init; }
    public TimeSpan Duration { get; init; }
}
