using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 人物与角色的关联
/// </summary>
public readonly record struct PersonCharacterRelation
{
    /// <summary>
    /// 人物 ID
    /// </summary>
    [JsonPropertyName("person_id")]
    public int PersonId { get; init; }

    /// <summary>
    /// 条目 ID
    /// </summary>
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; init; }

    /// <summary>
    /// 对应条目中的角色 ID
    /// </summary>
    [JsonPropertyName("character_id")]
    public int CharacterId { get; init; }

    /// <summary>
    /// 概要
    /// </summary>
    [JsonPropertyName("summary")]
    public string Summary { get; init; }
}
