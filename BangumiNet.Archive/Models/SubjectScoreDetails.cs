using System.Text.Json.Serialization;

namespace BangumiNet.Archive.Models;

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

    public int[] ToArray() => [S1, S2, S3, S4, S5, S6, S7, S8, S9, S10];
    public int Total() => ToArray().Sum();
    public int Average()
    {
        var array = ToArray();
        var sum = array.Sum();
        if (sum != 0)
            return array.Select(static (count, score) => count * (score + 1)).Sum() / sum;
        else
            return 0;
    }
}
