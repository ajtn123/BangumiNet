using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

/// <summary>
/// 番组数据
/// </summary>
public readonly record struct Item
{
    /// <summary>
    /// 番组原始标题
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// 番组标题翻译
    /// </summary>
    [JsonPropertyName("titleTranslate")]
    public Dictionary<Language, string[]> TitleTranslate { get; init; }

    /// <summary>
    /// 番组类型
    /// </summary>
    [JsonPropertyName("type")]
    public ItemType ItemType { get; init; }

    /// <summary>
    /// 番组语言
    /// </summary>
    [JsonPropertyName("lang")]
    public Language Language { get; init; }

    /// <summary>
    /// 官网
    /// </summary>
    [JsonPropertyName("officialSite")]
    public string OfficialSite { get; init; }

    /// <summary>
    /// tv/web: 番组开始时间; movie: 上映日期; ova: 首话发售时间.
    /// </summary>
    [JsonPropertyName("begin")]
    public DateTimeOffset? Begin { get; init; }

    /// <summary>
    /// 放送周期
    /// </summary>
    [JsonPropertyName("broadcast")]
    public RepeatingInterval? Broadcast { get; init; }

    /// <summary>
    /// tv/web: 番组完结时间; movie: 无意义; ova: 则为最终话发售时间（未确定则置空）
    /// </summary>
    [JsonPropertyName("end")]
    public DateTimeOffset? End { get; init; }

    /// <summary>
    /// 备注
    /// </summary>
    [JsonPropertyName("comment")]
    public string? Comment { get; init; }

    /// <summary>
    /// 站点
    /// </summary>
    [JsonPropertyName("sites")]
    public Site[] Sites { get; init; }
}
