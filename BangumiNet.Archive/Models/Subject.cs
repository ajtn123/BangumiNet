using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 条目
/// </summary>
public readonly record struct Subject
{
    /// <summary>
    /// 条目 ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// 条目类型
    /// </summary>
    [JsonPropertyName("type")]
    public int Type { get; init; }

    /// <summary>
    /// 条目名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// 条目简体中文名
    /// </summary>
    [JsonPropertyName("name_cn")]
    public string NameCn { get; init; }

    /// <summary>
    /// 条目原始 wiki 字符串
    /// </summary>
    [JsonPropertyName("infobox")]
    public string Infobox { get; init; }

    /// <summary>
    /// 条目平台
    /// </summary>
    [JsonPropertyName("platform")]
    public int Platform { get; init; }

    /// <summary>
    /// 条目简介
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; }

    /// <summary>
    /// 是否含有成人内容
    /// </summary>
    [JsonPropertyName("nsfw")]
    public bool IsNsfw { get; init; }

    /// <summary>
    /// 是否为系列作品
    /// </summary>
    [JsonPropertyName("series")]
    public bool IsSeries { get; init; }

    /// <summary>
    /// 发行日期
    /// </summary>
    [JsonPropertyName("date")]
    public DateOnly? ReleaseDate { get; init; }

    /// <summary>
    /// 收藏状态计数
    /// </summary>
    [JsonPropertyName("favorite")]
    public SubjectCollectionCounts Favorite { get; init; }

    /// <summary>
    /// 标签
    /// </summary>
    [JsonPropertyName("tags")]
    public Tag[] Tags { get; init; }

    /// <summary>
    /// 公共标签
    /// </summary>
    [JsonPropertyName("meta_tags")]
    public string[] MetaTags { get; init; }

    /// <summary>
    /// 评分
    /// </summary>
    [JsonPropertyName("score")]
    public double Score { get; init; }

    /// <summary>
    /// 评分
    /// </summary>
    [JsonPropertyName("score_details")]
    public SubjectScoreDetails ScoreDetails { get; init; }

    /// <summary>
    /// 类别内排名
    /// </summary>
    [JsonPropertyName("rank")]
    public int Rank { get; init; }
}
