using BangumiNet.Common.Extras;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 人物
/// </summary>
public readonly record struct Person
{
    /// <summary>
    /// 人物 ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// 人物名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// 人物类型
    /// </summary>
    [JsonPropertyName("type")]
    public PersonType Type { get; init; }

    /// <summary>
    /// 人物职业
    /// </summary>
    [JsonPropertyName("career")]
    public string[] Career { get; init; }

    /// <summary>
    /// 人物原始 wiki 字符串
    /// </summary>
    [JsonPropertyName("infobox")]
    public string Infobox { get; init; }

    /// <summary>
    /// 人物简介
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; }

    /// <summary>
    /// 评论数
    /// </summary>
    [JsonPropertyName("comments")]
    public int CommentCount { get; init; }

    /// <summary>
    /// 评论数
    /// </summary>
    [JsonPropertyName("collects")]
    public int CollectionCount { get; init; }
}
