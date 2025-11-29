using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public enum SiteType : byte
{
    [JsonStringEnumMemberName("info")]
    Info,
    [JsonStringEnumMemberName("onair")]
    OnAir,
    [JsonStringEnumMemberName("resource")]
    Resource,
}
