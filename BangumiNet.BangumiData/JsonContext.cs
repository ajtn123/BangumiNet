// If you need json source generation or AOT

/*

using BangumiNet.BangumiData.Converters;
using BangumiNet.BangumiData.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData;

[JsonSourceGenerationOptions(Converters = [
    typeof(JsonStringEnumConverter<SiteType>),
    typeof(JsonStringEnumConverter<ItemType>),
    typeof(JsonStringEnumConverter<Language>),
    typeof(RepeatingIntervalConverter),
    typeof(NullableConverterFactory)])]
[JsonSerializable(typeof(BangumiDataObject))]
public partial class JsonContext : JsonSerializerContext;

public static class Loader
{
    public static ValueTask<BangumiDataObject> LoadAsync(Stream stream)
        => JsonSerializer.DeserializeAsync(stream, JsonContext.Default.BangumiDataObject)!;
}

*/