using BangumiNet.Common;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 条目之间的关联 <c>subject-relations</c>
/// </summary>
public readonly record struct SubjectRelation
{
    /// <summary>
    /// 条目 ID
    /// </summary>
    [JsonPropertyName("subject_id")]
    public int SubjectId { get; init; }

    /// <summary>
    /// 关联类型
    /// </summary>
    [JsonPropertyName("relation_type")]
    public SubjectRelationType RelationType { get; init; }

    /// <summary>
    /// 关联条目 ID
    /// </summary>
    [JsonPropertyName("related_subject_id")]
    public int RelatedSubjectId { get; init; }

    /// <summary>
    /// 关联排序
    /// </summary>
    [JsonPropertyName("order")]
    public int Order { get; init; }
}
