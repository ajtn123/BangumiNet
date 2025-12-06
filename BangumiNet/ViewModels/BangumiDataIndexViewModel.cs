using BangumiNet.BangumiData.Models;

namespace BangumiNet.ViewModels;

public partial class BangumiDataIndexViewModel : ViewModelBase
{
    public BangumiDataIndexViewModel() => _ = Init();
    public async Task Init()
    {
        await BangumiDataProvider.LoadBangumiDataObject();
        Items = BangumiDataProvider.BangumiDataObject?.Items;
        Meta = BangumiDataProvider.BangumiDataObject?.SiteMeta;
    }

    [Reactive] public partial Item[]? Items { get; set; }
    [Reactive] public partial Dictionary<string, SiteMeta>? Meta { get; set; }
}
