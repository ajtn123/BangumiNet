using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 标签
/// </summary>
public readonly record struct Tag
{
    /// <summary>
    /// 标签名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// 添加数
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; init; }
}
