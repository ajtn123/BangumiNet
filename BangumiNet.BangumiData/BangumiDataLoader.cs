using BangumiNet.BangumiData.Converters;
using BangumiNet.BangumiData.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData;

public static class BangumiDataLoader
{
    private static readonly JsonSerializerOptions options = new()
    {
        Converters = { new JsonStringEnumConverter(), new NullableConverterFactory() }
    };

    public static ValueTask<BangumiDataObject> LoadAsync(Stream stream)
        => JsonSerializer.DeserializeAsync<BangumiDataObject>(stream, options);
    public static BangumiDataObject Load(string json)
        => JsonSerializer.Deserialize<BangumiDataObject>(json, options);
    public static ValueTask<Dictionary<string, SiteMeta>> LoadSitesAsync(Stream stream)
        => JsonSerializer.DeserializeAsync<Dictionary<string, SiteMeta>>(stream, options)!;
    public static Dictionary<string, SiteMeta> LoadSites(string json)
        => JsonSerializer.Deserialize<Dictionary<string, SiteMeta>>(json, options)!;
    public static ValueTask<Item[]> LoadItemsAsync(Stream stream)
        => JsonSerializer.DeserializeAsync<Item[]>(stream, options)!;
    public static Item[] LoadItems(string json)
        => JsonSerializer.Deserialize<Item[]>(json, options)!;
}
