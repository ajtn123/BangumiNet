using BangumiNet.Common;
using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

/// <summary>
/// 条目 <c>subject</c>
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
    public SubjectType Type { get; init; }

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
    /// 收藏统计信息
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
    /// 评分细节
    /// </summary>
    [JsonPropertyName("score_details")]
    public SubjectScoreDetails ScoreDetails { get; init; }

    /// <summary>
    /// 类别内排名
    /// </summary>
    [JsonPropertyName("rank")]
    public int Rank { get; init; }


    /// <summary>
    /// 收藏统计信息
    /// </summary>
    public readonly record struct SubjectCollectionCounts
    {
        /// <summary>
        /// 想看
        /// </summary>
        [JsonPropertyName("wish")]
        public int Wish { get; init; }

        /// <summary>
        /// 在看
        /// </summary>
        [JsonPropertyName("doing")]
        public int Doing { get; init; }

        /// <summary>
        /// 看过
        /// </summary>
        [JsonPropertyName("done")]
        public int Done { get; init; }

        /// <summary>
        /// 搁置
        /// </summary>
        [JsonPropertyName("on_hold")]
        public int OnHold { get; init; }

        /// <summary>
        /// 抛弃
        /// </summary>
        [JsonPropertyName("dropped")]
        public int Dropped { get; init; }
    }

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

    /// <summary>
    /// 评分细节
    /// </summary>
    public readonly record struct SubjectScoreDetails
    {
        [JsonPropertyName("1")]
        public int S1 { get; init; }
        [JsonPropertyName("2")]
        public int S2 { get; init; }
        [JsonPropertyName("3")]
        public int S3 { get; init; }
        [JsonPropertyName("4")]
        public int S4 { get; init; }
        [JsonPropertyName("5")]
        public int S5 { get; init; }
        [JsonPropertyName("6")]
        public int S6 { get; init; }
        [JsonPropertyName("7")]
        public int S7 { get; init; }
        [JsonPropertyName("8")]
        public int S8 { get; init; }
        [JsonPropertyName("9")]
        public int S9 { get; init; }
        [JsonPropertyName("10")]
        public int S10 { get; init; }


        /// <summary>
        /// 总投票人数
        /// </summary>
        public int TotalVoter => ToArray().Sum();

        /// <summary>
        /// 平均分
        /// </summary>
        public double Average()
        {
            var array = ToArray();
            var sum = array.Sum();
            if (sum != 0)
                return (double)array.Select(static (count, score) => count * (score + 1)).Sum() / sum;
            else
                return 0;
        }

        /// <summary>
        /// 转换为数组，第 0-9 个元素分别为打 1-10 分的人数
        /// </summary>
        public int[] ToArray() => [S1, S2, S3, S4, S5, S6, S7, S8, S9, S10];
    }
}
