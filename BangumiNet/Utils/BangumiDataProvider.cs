using BangumiNet.BangumiData;
using BangumiNet.BangumiData.Models;

namespace BangumiNet.Utils;

public static class BangumiDataProvider
{
    public static BangumiDataObject? BangumiDataObject { get; private set; }

    private static readonly FileInfo local = new(PathProvider.GetAbsolutePathForLocalData(Constants.BangumiDataJsonName));
    private static bool isLoading = false;
    public static async Task LoadBangumiDataObject(bool forceUpdate = false, CancellationToken cancellationToken = default)
    {
        if (!forceUpdate && BangumiDataObject != null) return;

        if (!local.Exists || DateTime.UtcNow - local.LastWriteTimeUtc > TimeSpan.FromDays(7))
            await UpdateBangumiDataAsync(cancellationToken);

        if (!local.Exists)
            return;

        if (isLoading) return; else isLoading = true;

        await using var json = local.OpenRead();
        BangumiDataObject = await BangumiDataLoader.LoadAsync(json);

        isLoading = false;
    }

    private static bool isUpdating = false;
    public static async Task UpdateBangumiDataAsync(CancellationToken cancellationToken = default)
    {
        if (isUpdating) return; else isUpdating = true;
        try
        {
            await using var response = await ApiC.HttpClient.GetStreamAsync(Constants.BangumiDataJsonUrl, cancellationToken);
            local.Directory?.Create();
            await using var file = local.Create();
            await response.CopyToAsync(file, CancellationToken.None);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
        finally { isUpdating = false; }
    }
}
