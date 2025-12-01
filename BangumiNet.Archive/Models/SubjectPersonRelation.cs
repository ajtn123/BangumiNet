using BangumiNet.Common;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 条目与人物的关联
/// </summary>
public readonly record struct SubjectPersonRelation
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
    /// 担任职位
    /// </summary>
    [JsonPropertyName("position")]
    public SubjectStaff Position { get; init; }

    /// <summary>
    /// 参与章节
    /// </summary>
    [JsonPropertyName("appear_eps")]
    public string AppearEps { get; init; }
}
