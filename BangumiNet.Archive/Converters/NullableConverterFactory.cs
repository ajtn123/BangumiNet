// Source - https://stackoverflow.com/a/65025191
// Posted by dbc, modified by community. See post 'Timeline' for change history
// Retrieved 2025-11-29, License - CC BY-SA 4.0

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Converters;

public class NullableConverterFactory : JsonConverterFactory
{
    private static readonly byte[] Empty = [];

    public override bool CanConvert(Type typeToConvert)
        => Nullable.GetUnderlyingType(typeToConvert) != null;

    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        => (JsonConverter)Activator.CreateInstance(typeof(NullableConverter<>).MakeGenericType(Nullable.GetUnderlyingType(type)!), BindingFlags.Instance | BindingFlags.Public, null, null, null)!;

    private class NullableConverter<T> : JsonConverter<T?> where T : struct
    {
        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
                if (reader.ValueTextEquals(Empty)) return null;

            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value!.Value, options);
    }
}
