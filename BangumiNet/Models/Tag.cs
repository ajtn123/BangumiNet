using BangumiNet.Api.Interfaces;

namespace BangumiNet.Models;

public class Tag : ITag
{
    public string? Name { get; set; }
    public int? Count { get; set; }
}
