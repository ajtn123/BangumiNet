using BangumiNet.Common;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 人物之间或角色之间的关联 <c>person-relations</c>
/// </summary>
public readonly record struct PersonRelation
{
    /// <summary>
    /// 现实人物或虚拟角色
    /// </summary>
    [JsonPropertyName("person_type")]
    public PersonCharacterType PersonType { get; init; }

    /// <summary>
    /// 人物 ID
    /// </summary>
    [JsonPropertyName("person_id")]
    public int PersonId { get; init; }

    /// <summary>
    /// 关联人物 ID
    /// </summary>
    [JsonPropertyName("related_person_id")]
    public int RelatedPersonId { get; init; }

    /// <summary>
    /// 关联类型
    /// </summary>
    [JsonPropertyName("relation_type")]
    public PersonCharacterRelationType RelationType { get; init; }

    /// <summary>
    /// 是否剧透
    /// </summary>
    [JsonPropertyName("spoiler")]
    public bool Spoiler { get; init; }

    /// <summary>
    /// 是否已结束
    /// </summary>
    [JsonPropertyName("ended")]
    public bool Ended { get; init; }


    /// <summary>
    /// 现实人物或虚拟角色
    /// </summary>
    public enum PersonCharacterType : byte
    {
        /// <summary>
        /// 现实人物
        /// </summary>
        [JsonStringEnumMemberName("prsn")]
        Person,

        /// <summary>
        /// 虚拟角色
        /// </summary>
        [JsonStringEnumMemberName("crt")]
        Character,
    }
}
