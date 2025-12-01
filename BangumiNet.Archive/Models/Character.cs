using BangumiNet.Common.Extras;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 角色
/// </summary>
public readonly record struct Character
{
    /// <summary>
    /// 角色 ID
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [JsonPropertyName("role")]
    public CharacterType Role { get; init; }

    /// <summary>
    /// 角色名
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; }

    /// <summary>
    /// 角色原始 wiki 字符串
    /// </summary>
    [JsonPropertyName("infobox")]
    public string Infobox { get; init; }

    /// <summary>
    /// 角色简介
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
