using System.Reactive.Linq;

namespace BangumiNet.Views;

public partial class LibraryFileCardView : ReactiveUserControl<LibraryFileViewModel>
{
    public LibraryFileCardView()
    {
        InitializeComponent();

        this.WhenAnyValue(x => x.ViewModel, x => x.ItemToggle.IsChecked)
            .Where(x => x.Item1 != null && (x.Item2 ?? false))
            .Take(1)
            .Subscribe(async x => await x.Item1!.LoadItems());

        FileName.DoubleTapped += (s, e) => { if (ViewModel?.FileInfo?.FullName is string path) CommonUtils.OpenUri(path); };
    }
}