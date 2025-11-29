using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

/// <summary>
/// 站点元数据
/// </summary>
public readonly record struct SiteMeta
{
    /// <summary>
    /// 站点名称
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; init; }

    /// <summary>
    /// 站点 url 模板
    /// </summary>
    [JsonPropertyName("urlTemplate")]
    public string UrlTemplate { get; init; }

    /// <summary>
    /// 站点区域限制，主要针对 onAir 类型的放送站点。如无该字段，表明该站点无区域限制
    /// </summary>
    [JsonPropertyName("regions")]
    public IReadOnlyList<string>? Regions { get; init; }

    /// <summary>
    /// 站点类型: info, onair, resource
    /// </summary>
    [JsonPropertyName("type")]
    public SiteType Type { get; init; }
}
