using System.Globalization;
using System.Text.Json.Serialization;

namespace BangumiNet.BangumiData.Models;

public enum Language : byte
{
    /// <summary>
    /// ja
    /// </summary>
    [JsonStringEnumMemberName("ja")]
    Japanese,

    /// <summary>
    /// en
    /// </summary>
    [JsonStringEnumMemberName("en")]
    English,

    /// <summary>
    /// zh-Hans
    /// </summary>
    [JsonStringEnumMemberName("zh-Hans")]
    ChineseSimplified,

    /// <summary>
    /// zh-Hant
    /// </summary>
    [JsonStringEnumMemberName("zh-Hant")]
    ChineseTraditional,
}

public static partial class EnumExtensions
{
    public static CultureInfo ToCulture(this Language lang) => lang switch
    {
        Language.Japanese => new CultureInfo("ja"),
        Language.English => new CultureInfo("en"),
        Language.ChineseSimplified => new CultureInfo("zh-Hans"),
        Language.ChineseTraditional => new CultureInfo("zh-Hant"),
        _ => CultureInfo.InvariantCulture,
    };
}
