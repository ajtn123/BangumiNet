using Avalonia.Platform;
using BangumiNet.Models;
using System.Text.Json;

namespace BangumiNet.Utils;

public static class LicenseProvider
{
    private static readonly Uri ResourceFolder = new($"avares://BangumiNet/Assets/Licenses");

    private static string GetLicenseText(string name)
    {
        using var stream = AssetLoader.Open(new($"{ResourceFolder.OriginalString}/{name}.txt"));
        using var reader = new StreamReader(stream, System.Text.Encoding.UTF8);
        return reader.ReadToEnd();
    }

    private static string MIT { get => field ??= GetLicenseText("MIT"); }
    private static string Apache20 { get => field ??= GetLicenseText("Apache20"); }
    private static string MSPL { get => field ??= GetLicenseText("MSPL"); }

    private static IEnumerable<LicenseItem> GetResource()
    {
        var result = new List<LicenseItem>();
        foreach (var uri in AssetLoader.GetAssets(ResourceFolder, null).Where(x => x.AbsolutePath.EndsWith(".json")))
            try
            {
                using var stream = AssetLoader.Open(uri);
                if (JsonSerializer.Deserialize<List<LicenseItem>>(stream) is { Count: > 0 } items)
                    result.AddRange(items.Select(item => item switch
                    {
                        { LicenseText.Length: > 0 } => item,
                        { License: "MIT" or "mit" } => item with { LicenseText = MIT },
                        { License: "Apache 2.0" or "Apache-2.0" } => item with { LicenseText = Apache20 },
                        { License: "MS-PL" } => item with { LicenseText = MSPL },
                        _ => item
                    }));
            }
            catch (Exception ex) { ex.TraceWarning($"Failed to open license record: {uri}"); }
        return result.DistinctBy(x => x.Name);
    }

    private static IEnumerable<LicenseItem> GetCredits() =>
    [
        new("Bangumi Open API", "服务", null, null, null, "https://bangumi.github.io/api/#/", null, null),
        new("Bangumi Private API", "服务", null, null, null, "https://next.bgm.tv/p1/#/", null, null),
        new("Bangumi Stickers", "资产", null, null, null, "https://bgm.tv", null, null),
        new("Bangumi Data", "服务", null, null, null, "https://github.com/bangumi-data/bangumi-data", null, null),
        new("Fluent UI System Icons", "资产", null, null, null, "https://github.com/microsoft/fluentui-system-icons", null, null),
        new("はらぺこ 何番煎じだかわからないけど", "资产", null, null, null, "https://www.pixiv.net/artworks/22876424", null, null),
        new("MingCute Icon tv-2-line", "资产", "2.97", "Apache 2.0", "https://raw.githubusercontent.com/mingcute-design/mingcute-icons/refs/heads/main/LICENSE", "https://www.mingcute.com", "MingCute Design", Apache20),
    ];

    public static LicenseItem[] Items { get => field ??= [.. GetCredits(), .. GetResource()]; }
}
