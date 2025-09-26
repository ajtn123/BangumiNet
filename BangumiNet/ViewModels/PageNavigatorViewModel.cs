using System.Reactive;
using System.Reactive.Linq;

namespace BangumiNet.ViewModels;

public partial class PageNavigatorViewModel : ViewModelBase
{
    public PageNavigatorViewModel()
    {
        this.WhenAnyValue(x => x.PageIndex).Subscribe(y => PageIndexInput = y);
        PrevPage = ReactiveCommand.Create(() => (int)PageIndex! - 1, this.WhenAnyValue(x => x.Total, x => x.PageIndex).Select(y => IsInRange(PageIndex - 1)));
        NextPage = ReactiveCommand.Create(() => (int)PageIndex! + 1, this.WhenAnyValue(x => x.Total, x => x.PageIndex).Select(y => IsInRange(PageIndex + 1)));
        JumpPage = ReactiveCommand.Create(() => (int)PageIndexInput!, this.WhenAnyValue(x => x.Total, x => x.PageIndexInput).Select(y => IsInRange(PageIndexInput)));
    }

    [Reactive] public partial int? PageIndex { get; set; }
    [Reactive] public partial int? PageIndexInput { get; set; }
    [Reactive] public partial int? Total { get; set; }

    public ReactiveCommand<Unit, int> PrevPage { get; set; }
    public ReactiveCommand<Unit, int> NextPage { get; set; }
    public ReactiveCommand<Unit, int> JumpPage { get; set; }

    public bool IsInRange(int? d)
    {
        if (d == null || PageIndex == null || Total == null) return false;
        else return d > 0 && d <= Total;
    }
}
