using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Auxiliary;

/// <summary>
/// Wiki 导出信息 <see href="https://raw.githubusercontent.com/bangumi/Archive/refs/heads/master/aux/latest.json">https://raw.githubusercontent.com/bangumi/Archive/refs/heads/master/aux/latest.json</see>
/// </summary>
public record class DumpInformation
{
    public const string LatestDumpInformationUrl = "https://raw.githubusercontent.com/bangumi/Archive/refs/heads/master/aux/latest.json";


    [JsonPropertyName("browser_download_url")]
    public string? BrowserDownloadUrl { get; init; }

    [JsonPropertyName("content_type")]
    public string? ContentType { get; init; }

    [JsonPropertyName("created_at")]
    public DateTimeOffset CreatedAt { get; init; }

    [JsonPropertyName("digest")]
    public string? Digest { get; init; }

    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("label")]
    public string? Label { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("node_id")]
    public string? NodeId { get; init; }

    [JsonPropertyName("size")]
    public long Size { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTimeOffset UpdatedAt { get; init; }

    [JsonPropertyName("url")]
    public string? Url { get; init; }
}
