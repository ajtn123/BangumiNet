using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

/// <summary>
/// 站点
/// </summary>
public readonly record struct Site
{
    /// <summary>
    /// 站点 name，请和最外层 sites 字段中的元数据对应
    /// </summary>
    [JsonPropertyName("site")]
    public string Name { get; init; }

    /// <summary>
    /// 站点 id，可用于替换模板中相应的字段
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; init; }

    /// <summary>
    /// url，如果当前url不符合urlTemplate中的规则时使用，优先级高于id
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>
    /// 放送开始时间
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
    /// 番剧放送站点区域限制，用于覆盖站点本身的区域限制
    /// </summary>
    [JsonPropertyName("regions")]
    public string[]? Regions { get; init; }

    public string? GetUrl(Dictionary<string, SiteMeta> meta)
    {
        if (!string.IsNullOrWhiteSpace(Url))
            return Url;
        else if (meta.TryGetValue(Name, out var siteMeta))
            return siteMeta.UrlTemplate.Replace("{{id}}", Id);
        else
            return null;
    }
}
