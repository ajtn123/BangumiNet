using BangumiNet.BangumiData;
using BangumiNet.BangumiData.Models;
using System.Net.Http;

namespace BangumiNet.ViewModels;

public partial class BangumiDataIndexViewModel : ViewModelBase
{
    public static BangumiDataObject? BangumiDataObject { get; private set; }
    public static async Task LoadBangumiDataObject(bool forceUpdate = false)
    {
        if (!forceUpdate && BangumiDataObject != null) return;
        try
        {
            using var json = await new HttpClient().GetStreamAsync(Constants.BangumiDataJsonUrl);
            BangumiDataObject = await BangumiDataLoader.LoadAsync(json);
        }
        catch (Exception e) { Trace.TraceError(e.Message); }
    }

    public async Task Init()
    {
        if (Items != null) return;
        await LoadBangumiDataObject();
        Items = BangumiDataObject?.Items;
    }

    [Reactive] public partial Item[]? Items { get; set; }
}
