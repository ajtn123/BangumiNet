using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public enum ItemType : byte
{
    [JsonStringEnumMemberName("tv")]
    TV = 1,
    [JsonStringEnumMemberName("ova")]
    OVA = 2,
    [JsonStringEnumMemberName("movie")]
    Movie = 3,
    [JsonStringEnumMemberName("web")]
    WEB = 5,
}
