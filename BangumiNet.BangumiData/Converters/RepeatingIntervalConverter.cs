using BangumiNet.BangumiData.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace BangumiNet.BangumiData.Converters;

// https://en.wikipedia.org/wiki/ISO_8601#Repeating_intervals
// https://github.com/bangumi-data/bangumi-data?tab=contributing-ov-file#%E5%85%B3%E4%BA%8Ebroadcast%E5%AD%97%E6%AE%B5
public class RepeatingIntervalConverter : JsonConverter<RepeatingInterval>
{
    public override RepeatingInterval Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
    {
        var raw = reader.ValueSpan;

        ReadOnlySpan<byte> startBytes = raw.Slice(2, 24);
        ReadOnlySpan<byte> durationBytes = raw[27..];

        return new RepeatingInterval
        {
            Start = DateTimeOffset.Parse(Encoding.UTF8.GetString(startBytes)),
            Duration = XmlConvert.ToTimeSpan(Encoding.UTF8.GetString(durationBytes))
        };
    }

    public override void Write(Utf8JsonWriter writer, RepeatingInterval value, JsonSerializerOptions options)
        => writer.WriteStringValue($"R/{value.Start:O}/{XmlConvert.ToString(value.Duration)}");
}
