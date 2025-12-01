using BangumiNet.Common.Extras;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 章节
/// </summary>
public readonly record struct Episode
{
    /// <summary>
    /// 章节 ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// 章节名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// 章节简体中文名
    /// </summary>
    [JsonPropertyName("name_cn")]
    public string NameCn { get; init; }

    /// <summary>
    /// 章节介绍
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; init; }

    /// <summary>
    /// 播出时间
    /// </summary>
    [JsonPropertyName("airdate")]
    public string AirDate { get; init; }

    /// <summary>
    /// 该章节存在于第几张光盘
    /// </summary>
    [JsonPropertyName("disc")]
    public int DiscNumber { get; init; }

    /// <summary>
    /// 播放时长
    /// </summary>
    [JsonPropertyName("duration")]
    public string Duration { get; init; }

    /// <summary>
    /// 条目 ID
    /// </summary>
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; init; }

    /// <summary>
    /// 序话
    /// </summary>
    [JsonPropertyName("sort")]
    public double Sort { get; init; }

    /// <summary>
    /// 类型
    /// </summary>
    [JsonPropertyName("type")]
    public EpisodeType Type { get; init; }
}
