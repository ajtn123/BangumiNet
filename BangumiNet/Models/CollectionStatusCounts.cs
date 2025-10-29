using BangumiNet.Api.Interfaces;

namespace BangumiNet.Models;

public class CollectionStatusCounts : ICollection
{
    public int? Collect { get; set; }
    public int? Doing { get; set; }
    public int? Dropped { get; set; }
    public int? OnHold { get; set; }
    public int? Wish { get; set; }
}
