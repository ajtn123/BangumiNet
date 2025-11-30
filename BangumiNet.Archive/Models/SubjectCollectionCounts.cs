using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

public readonly record struct SubjectCollectionCounts
{
    /// <summary>
    /// 想看
    /// </summary>
    [JsonPropertyName("wish")]
    public int Wish { get; init; }

    /// <summary>
    /// 在看
    /// </summary>
    [JsonPropertyName("doing")]
    public int Doing { get; init; }

    /// <summary>
    /// 看过
    /// </summary>
    [JsonPropertyName("done")]
    public int Done { get; init; }

    /// <summary>
    /// 搁置
    /// </summary>
    [JsonPropertyName("on_hold")]
    public int OnHold { get; init; }

    /// <summary>
    /// 抛弃
    /// </summary>
    [JsonPropertyName("dropped")]
    public int Dropped { get; init; }
}
