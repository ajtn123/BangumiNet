using BangumiNet.Common.Extras;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 条目与角色的关联
/// </summary>
public readonly record struct SubjectCharacterRelation
{
    /// <summary>
    /// 对应条目中的角色 ID
    /// </summary>
    [JsonPropertyName("character_id")]
    public int CharacterId { get; init; }

    /// <summary>
    /// 条目 ID
    /// </summary>
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; init; }

    /// <summary>
    /// 角色类型
    /// </summary>
    [JsonPropertyName("type")]
    public CharacterRole Type { get; init; }

    /// <summary>
    /// 作品角色列表排序
    /// </summary>
    [JsonPropertyName("order")]
    public int Order { get; init; }
}
