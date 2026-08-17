using Avalonia.Platform;
using BangumiNet.BangumiData.Models;
using BangumiNet.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace BangumiNet.Utils;

public static class LicenseProvider
{
    private static readonly Uri ResourceFolder = new($"avares://BangumiNet/Assets/Licenses");

    private static IEnumerable<LicenseItem> GetResource()
    {
        var result = new List<LicenseItem>();
        foreach (var uri in AssetLoader.GetAssets(ResourceFolder, null))
            try
            {
                using var stream = AssetLoader.Open(uri);
                if (JsonSerializer.Deserialize<List<LicenseItem>>(stream) is { Count: > 0 } items)
                    result.AddRange(items);
            }
            catch (Exception ex) { ex.TraceWarning($"Failed to open license record: {uri}"); }
        return result.DistinctBy(x => x.Name);
    }

    public static LicenseItem[] Items { get => field ??= [.. GetResource()]; }
}
