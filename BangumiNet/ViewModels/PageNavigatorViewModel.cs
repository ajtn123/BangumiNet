using BangumiNet.Api.Interfaces;
using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class PageNavigatorViewModel : ViewModelBase
{
    public PageNavigatorViewModel()
    {
        CurrentPage = 1;
        InputPage = 1;
        TotalPages = 1;

        this.WhenAnyValue(x => x.CurrentPage).Subscribe(y => InputPage = y);
        this.WhenAnyValue(x => x.TotalPages).Subscribe(y => this.RaisePropertyChanged(nameof(IsSinglePage)));
        PrevPage = ReactiveCommand.Create(() => (int)CurrentPage! - 1, this.WhenAnyValue(x => x.TotalPages, x => x.CurrentPage).Select(y => IsInRange(CurrentPage - 1)));
        NextPage = ReactiveCommand.Create(() => (int)CurrentPage! + 1, this.WhenAnyValue(x => x.TotalPages, x => x.CurrentPage).Select(y => IsInRange(CurrentPage + 1)));
        JumpPage = ReactiveCommand.Create(() => (int)InputPage!, this.WhenAnyValue(x => x.TotalPages, x => x.InputPage).Select(y => IsInRange(InputPage)));
    }

    public void UpdatePageInfo(IPagedResponseFull response)
    {
        PageSize = response.Limit;
        CurrentOffset = response.Offset;
        TotalItems = response.Total;

        if (response.Offset != null && response.Limit != null)
            CurrentPage = response.Offset / response.Limit + 1;
        else CurrentPage = null;

        if (response.Total != null && response.Limit != null)
            TotalPages = (int)Math.Ceiling((double)response.Total / (double)response.Limit);
        else TotalPages = null;
    }
    public void UpdatePageInfo(int? limit, int? offset, int? total)
    {
        PageSize = limit;
        CurrentOffset = offset;
        TotalItems = total;

        if (offset != null && limit != null)
            CurrentPage = offset / limit + 1;
        else CurrentPage = null;

        if (total != null && limit != null)
            TotalPages = (int)Math.Ceiling((double)total / (double)limit);
        else TotalPages = null;
    }

    [Reactive] public partial int? PageSize { get; set; }
    [Reactive] public partial int? CurrentOffset { get; set; }
    [Reactive] public partial int? TotalItems { get; set; }

    [Reactive] public partial int? CurrentPage { get; set; }
    [Reactive] public partial int? InputPage { get; set; }
    [Reactive] public partial int? TotalPages { get; set; }

    public ReactiveCommand<Unit, int> PrevPage { get; set; }
    public ReactiveCommand<Unit, int> NextPage { get; set; }
    public ReactiveCommand<Unit, int> JumpPage { get; set; }

    public bool IsInRange(int? d)
    {
        if (d == 1) return true;
        else if (d == null || CurrentPage == null || TotalPages == null) return false;
        else return d > 0 && d <= TotalPages;
    }

    public bool IsSinglePage => TotalPages == 1;
}
