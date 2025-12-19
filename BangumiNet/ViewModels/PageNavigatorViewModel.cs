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
        PrevPage = ReactiveCommand.Create(() => (int)CurrentPage! - 1, this.WhenAnyValue(x => x.TotalPages, x => x.CurrentPage).Select(y => IsInRange(CurrentPage - 1)));
        NextPage = ReactiveCommand.Create(() => (int)CurrentPage! + 1, this.WhenAnyValue(x => x.TotalPages, x => x.CurrentPage).Select(y => IsInRange(CurrentPage + 1)));
        JumpPage = ReactiveCommand.Create(() => (int)InputPage!, this.WhenAnyValue(x => x.TotalPages, x => x.InputPage).Select(y => IsInRange(InputPage)));
    }

    public void UpdatePageInfo(IPagedResponseFull response)
        => UpdatePageInfo(response.Limit, response.Offset, response.Total);
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

        this.RaisePropertyChanged(nameof(IsSinglePage));
        this.RaisePropertyChanged(nameof(RangeAndTotalMessage));
    }

    [Reactive] public partial int? PageSize { get; private set; }
    [Reactive] public partial int? CurrentOffset { get; private set; }
    [Reactive] public partial int? TotalItems { get; private set; }

    [Reactive] public partial int? CurrentPage { get; private set; }
    [Reactive] public partial int? InputPage { get; set; }
    [Reactive] public partial int? TotalPages { get; private set; }

    public ReactiveCommand<Unit, int> PrevPage { get; init; }
    public ReactiveCommand<Unit, int> NextPage { get; init; }
    public ReactiveCommand<Unit, int> JumpPage { get; init; }

    public bool IsInRange(int? d)
    {
        if (d == 1) return true;
        else if (d == null || CurrentPage == null || TotalPages == null) return false;
        else return d > 0 && d <= TotalPages;
    }

    public bool IsSinglePage => TotalPages == 1;
    public string RangeAndTotalMessage
    {
        get
        {
            var range = CurrentOffset == null || PageSize == null ? null : $"第 {CurrentOffset + 1}-{Math.Min((int)(CurrentOffset + PageSize), TotalItems ?? int.MaxValue)} 个项目";
            var total = TotalItems == null ? null : $"共 {TotalItems} 个";
            var strs = ((string?[])[range, total]).OfType<string>();
            return string.Join('，', strs);
        }
    }
}
