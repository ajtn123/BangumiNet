using System.Text.RegularExpressions;

namespace BangumiNet.Api.Misc;

public static partial class BangumiImage
{
    public const string Host = "https://lain.bgm.tv";

    [GeneratedRegex(@"^https://lain\.bgm\.tv/")]
    private static partial Regex BangumiImageUrlPattern();
    [GeneratedRegex(@"^https://lain\.bgm\.tv/r/[0-9x]+/")]
    private static partial Regex BangumiResizedImageUrlPattern();

    public static bool IsBangumiImage(string url) => BangumiImageUrlPattern().IsMatch(url);
    public static bool IsBangumiResizedImage(string url) => BangumiResizedImageUrlPattern().IsMatch(url);

    public static string GetOriginalImage(string url)
    {
        if (IsBangumiResizedImage(url))
            return BangumiResizedImageUrlPattern().Replace(url, $"{Host}/");

        if (IsBangumiImage(url))
            return url;

        throw new ArgumentException("Url is not a bangumi image.", nameof(url));
    }

    /// <summary>
    /// 有效宽高值：
    /// <list type="bullet">
    /// <item>0 (保持宽高比)</item>
    /// <item>100</item>
    /// <item>200</item>
    /// <item>400</item>
    /// <item>600</item>
    /// <item>800</item>
    /// <item>1200</item>
    /// </list>
    /// 详见 https://github.com/bangumi/img-proxy
    /// </summary>
    /// <param name="url">bangumi 图片地址</param>
    /// <param name="width">目标宽度</param>
    /// <param name="height">目标高度</param>
    /// <returns>缩放/裁剪后的图片地址</returns>
    /// <exception cref="ArgumentException"></exception>
    public static string GetResizedImage(string url, int width = 0, int height = 0)
    {
        var resizer = $"{Host}/r/{width}x{height}/";

        if (IsBangumiResizedImage(url))
            return BangumiResizedImageUrlPattern().Replace(url, resizer);

        if (IsBangumiImage(url))
            return BangumiImageUrlPattern().Replace(url, resizer);

        throw new ArgumentException("Url is not a bangumi image.", nameof(url));
    }
}
