using BangumiNet.Api.Interfaces;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class PageNavigatorViewModel : ViewModelBase
{
    public PageNavigatorViewModel()
    {
        PageIndex = 1;
        PageIndexInput = 1;
        Total = 1;

        this.WhenAnyValue(x => x.PageIndex).Subscribe(y => PageIndexInput = y);
        PrevPage = ReactiveCommand.Create(() => (int)PageIndex! - 1, this.WhenAnyValue(x => x.Total, x => x.PageIndex).Select(y => IsInRange(PageIndex - 1)));
        NextPage = ReactiveCommand.Create(() => (int)PageIndex! + 1, this.WhenAnyValue(x => x.Total, x => x.PageIndex).Select(y => IsInRange(PageIndex + 1)));
        JumpPage = ReactiveCommand.Create(() => (int)PageIndexInput!, this.WhenAnyValue(x => x.Total, x => x.PageIndexInput).Select(y => IsInRange(PageIndexInput)));
    }

    public void UpdatePageInfo(IPagedResponse response)
    {
        if (response.Offset != null && response.Limit != null)
            PageIndex = response.Offset / response.Limit + 1;
        else PageIndex = null;

        if (response.Total != null && response.Limit != null)
            Total = (int)Math.Ceiling((double)response.Total / (double)response.Limit);
        else Total = null;
    }
    public void UpdatePageInfo(int? limit, int? offset, int? total)
    {
        if (offset != null && limit != null)
            PageIndex = offset / limit + 1;
        else PageIndex = null;

        if (total != null && limit != null)
            Total = (int)Math.Ceiling((double)total / (double)limit);
        else Total = null;
    }

    [Reactive] public partial int? PageIndex { get; set; }
    [Reactive] public partial int? PageIndexInput { get; set; }
    [Reactive] public partial int? Total { get; set; }

    public ReactiveCommand<Unit, int> PrevPage { get; set; }
    public ReactiveCommand<Unit, int> NextPage { get; set; }
    public ReactiveCommand<Unit, int> JumpPage { get; set; }

    public bool IsInRange(int? d)
    {
        if (d == 1) return true;
        else if (d == null || PageIndex == null || Total == null) return false;
        else return d > 0 && d <= Total;
    }
}
