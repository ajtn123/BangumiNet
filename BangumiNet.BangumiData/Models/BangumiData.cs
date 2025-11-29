using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public readonly record struct BangumiDataObject
{
    [JsonPropertyName("siteMeta")]
    public Dictionary<string, SiteMeta> SiteMeta { get; init; }

    [JsonPropertyName("items")]
    public Item[] Items { get; init; }
}
