using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public record class BangumiDataObject
{
    [JsonPropertyName("siteMeta")]
    public required Dictionary<string, SiteMeta> SiteMeta { get; init; }

    [JsonPropertyName("items")]
    public required Item[] Items { get; init; }
}
