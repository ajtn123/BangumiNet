namespace BangumiNet.Models.ItemNetwork;

public class Relationship
{
    public required Node From { get; init; }
    public required Node To { get; init; }
    public required string Relation { get; init; }
}
