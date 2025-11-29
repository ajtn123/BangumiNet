using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public enum ItemType : byte
{
    [JsonStringEnumMemberName("tv")]
    TV,
    [JsonStringEnumMemberName("web")]
    WEB,
    [JsonStringEnumMemberName("movie")]
    Movie,
    [JsonStringEnumMemberName("ova")]
    OVA,
}
